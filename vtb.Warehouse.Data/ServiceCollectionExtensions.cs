using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vtb.Warehouse.Data.Database;
using vtb.Warehouse.Data.Repositories;
using AutoMapper;
using System.Reflection;

namespace vtb.Warehouse.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWarehouses(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper();
            services.AddDbContext<WarehouseContext>(o => o.UseSqlServer(config["WarehouseContext"]));

            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IRackService, RackService>();

            return services;
        }
    }
}
