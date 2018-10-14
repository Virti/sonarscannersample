using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using vtb.Core.Utils.Extensions;
using vtb.Core.Utils.Results;
using vtb.Warehouse.Data.Database;
using vtb.Warehouse.Data.Database.Model;
using vtb.Warehouse.Data.MaperModels;

namespace vtb.Warehouse.Data.Repositories
{
    public class BuildingService : IBuildingService
    {
        private readonly IDistributedCache _cache;
        private readonly WarehouseContext _context;
        private readonly IMapper _mapper;
        private readonly IRackService _rackService;

        private const string DEFAULT_ORDER = "-Label";

        private const string CACHE_BUILDING_VOLUME_SUMMARY = "building.{0}.volumeSummary";

        public BuildingService(IRackService rackService, WarehouseContext context, IDistributedCache cache, IMapper mapper)
        {
            _rackService = rackService;
            _context = context;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<PagedResult<BuildingListItem>> GetBuildings(int page, int perPage, string order = "", string filter = "")
        {
            order = order.ParseOrderStringForDynamicLinq(DEFAULT_ORDER);

            var totalCnt = _context.Buildings.CountAsync();
            var buildings = _context.Buildings
                .OrderBy(order)
                .Skip(page * perPage)
                .Take(perPage)
                .ProjectTo<BuildingListItem>(_mapper.ConfigurationProvider).ToListAsync();

            return new PagedResult<BuildingListItem>
            {
                CurrentPage = page,
                PerPage = perPage,

                AllRecords = await totalCnt,
                Records = await buildings
            };
        }

        public async Task<BuildingSummary> GetBuildingSummary(Guid id)
        {
            var buildingDetails = await _context.Buildings.ProjectTo<BuildingDetails>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(b => b.Id == id);
            if (buildingDetails == null)
                return null;

            var volumeSummary = await GetBuldingVolumeSummary(id); 

            return new BuildingSummary()
            {
                Details = buildingDetails,
                VolumeSummary = volumeSummary
            };
        }

        public async Task<BuildingDetails> Insert(BuildingDetails details)
        {
            var building = _mapper.Map<BuildingDetails, Building>(details);

            await _context.Buildings.AddAsync(building);
            await _context.SaveChangesAsync();

            return _mapper.Map<BuildingDetails>(building);
        }

        public async Task<BuildingDetails> Update(BuildingDetails details)
        {
            var building = await _context.Buildings.FirstOrDefaultAsync(b => b.Id == details.Id);
            if (building == null)
                throw new BuildingDoesNotExistException(details.Id);

            _mapper.Map<BuildingDetails, Building>(details, building);
            await _context.SaveChangesAsync();

            return _mapper.Map<BuildingDetails>(building);
        }

        public async Task Delete(Guid id)
        {
            var building = new Building() { Id = id };
            _context.Buildings.Attach(building);
            _context.Buildings.Remove(building);

            try
            {
                await _context.SaveChangesAsync();

                // remove all cache entries
                await _cache.RemoveAsync(string.Format(CACHE_BUILDING_VOLUME_SUMMARY, id));
            }
            catch (DbUpdateConcurrencyException)
            {
                // this means that we probably try to delete an entity that already does not exist
                building = await _context.Buildings.FirstOrDefaultAsync(b => b.Id == id);
                if(building != null)
                {
                    // actually, building exists, so reason for exception is unknown
                    throw;
                }
            }
        }

        private async Task<BuildingVolumeSummary> GetBuldingVolumeSummary(Guid id)
        {
            // since this can eventually operate on large sets of data, save summary in cache
            var cacheKey = string.Format(CACHE_BUILDING_VOLUME_SUMMARY, id);
            var rackData = await _cache.GetOrUpdate(cacheKey, async () =>
            {
                return await _rackService.GetVolumeSummaryForBuilding(id);
            }, TimeSpan.FromHours(1));
            return rackData;
        }
    }

    [Serializable]
    public class BuildingDoesNotExistException : Exception
    {
        private Guid Id;

        public BuildingDoesNotExistException()
        {
        }

        public BuildingDoesNotExistException(Guid id) : base($"Building with ID \"{id}\" does not exist.")
        {
            this.Id = id;
        }
    }
}
