using System;
using System.Threading.Tasks;
using vtb.Warehouse.Data.MaperModels;

namespace vtb.Warehouse.Data.Repositories
{
    public interface IRackService
    {
        Task<BuildingVolumeSummary> GetVolumeSummaryForBuilding(Guid buildingId);
    }
}
