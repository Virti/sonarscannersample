using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vtb.Warehouse.Data;
using vtb.Warehouse.Data.Database;
using vtb.Warehouse.Data.Repositories;

namespace vtb.Api.Controllers.Warehouses
{
    [Route("api/warehouses/[controller]")]
    public abstract class VtbWarehousesController : VtbController
    {
    }
}
