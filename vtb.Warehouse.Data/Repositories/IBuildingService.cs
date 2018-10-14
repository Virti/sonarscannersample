using System;
using System.Threading.Tasks;
using vtb.Core.Utils.Results;
using vtb.Warehouse.Data.MaperModels;

namespace vtb.Warehouse.Data.Repositories
{
    public interface IBuildingService
    {
        Task<PagedResult<BuildingListItem>> GetBuildings(int page, int perPage, string order = "", string filter = "");
        Task<BuildingSummary> GetBuildingSummary(Guid id);
        Task<BuildingDetails> Insert(BuildingDetails details);
        Task<BuildingDetails> Update(BuildingDetails details);
        Task Delete(Guid id);
    }
}
