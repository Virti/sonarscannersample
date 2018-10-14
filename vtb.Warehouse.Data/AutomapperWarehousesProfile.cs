using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using vtb.Warehouse.Data.Database.Model;
using vtb.Warehouse.Data.MaperModels;

namespace vtb.Warehouse.Data
{
    public class AutomapperWarehousesProfile : Profile
    {
        public AutomapperWarehousesProfile()
        {
            CreateMap<Building, BuildingListItem>();
            CreateMap<Building, BuildingDetails>()
                .ReverseMap()
                .ForMember(building => building.Racks, o => o.Ignore()); // this should prevent us from accidentally clearing out all racks
        }
    }
}
