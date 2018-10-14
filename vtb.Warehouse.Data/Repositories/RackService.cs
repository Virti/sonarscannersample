using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using vtb.Warehouse.Data.Database;
using vtb.Warehouse.Data.MaperModels;

namespace vtb.Warehouse.Data.Repositories
{
    public class RackService : IRackService
    {
        private readonly WarehouseContext _context;
        private readonly IMapper _mapper;

        public RackService(WarehouseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BuildingVolumeSummary> GetVolumeSummaryForBuilding(Guid buildingId)
        {
            var totalVolumeQuery = _context.Racks
                .Where(r => r.BuildingId == buildingId)
                .Join(_context.Shelves, r => r.Id, s => s.RackId, (r, s) => new { Rack = r, Shelf = s })
                
                .Select(x => x.Shelf.Width * x.Shelf.Height * x.Shelf.Depth)

                .SumAsync();

            var volumeTakenQuery = _context.Racks
                .Where(r => r.BuildingId == buildingId)
                .Join(_context.Shelves, r => r.Id, s => s.RackId, (r, s) => new { Rack = r, Shelf = s })
                .Join(_context.StorageUnits, x => x.Shelf.Id, su => su.ShelfId, (x, su) => new { StorageUnit = su })
                .Join(_context.Units, x => x.StorageUnit.UnitId, u => u.Id, (x, u) => new { x.StorageUnit.Quantity, Unit = u })

                .Select(x => x.Quantity * x.Unit.Width * x.Unit.Height * x.Unit.Depth)

                .SumAsync();


            return new BuildingVolumeSummary()
            {
                BuildingId = buildingId,
                TotalVolume = await totalVolumeQuery,
                VolumeTaken = await volumeTakenQuery
            };
        }
    }
}
