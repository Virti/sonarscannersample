using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using vtb.Core.Utils.Requests;
using vtb.Core.Utils.Results;
using vtb.Warehouse.Data.MaperModels;
using vtb.Warehouse.Data.Repositories;

namespace vtb.Api.Controllers.Warehouses
{
    public class BuildingController : VtbWarehousesController
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<BuildingListItem>>> Get([FromQuery]PagedRequest pagination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _buildingService.GetBuildings(pagination.Page, pagination.PerPage, pagination.Order);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingSummary>> Get(Guid id)
        {
            var summary = await _buildingService.GetBuildingSummary(id);

            if (summary == null)
                return NotFound();

            return summary;
        }

        [HttpPut]
        public async Task<ActionResult<BuildingDetails>> Put(BuildingDetails details)
        {
            try
            {
                return await _buildingService.Update(details);
            }
            catch (BuildingDoesNotExistException e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<BuildingDetails>> Post(BuildingDetails details)
        {
            return await _buildingService.Insert(details);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _buildingService.Delete(id);
            return Ok();
        }
    }
}