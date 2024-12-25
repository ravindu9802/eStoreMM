using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Domain.Repositories;
using Orders.Infrastructure.Persistance;
using Orders.Infrastructure.Repositories;

namespace Orders.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddOrdersInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddNpgsql<OrderDbContext>(configuration.GetConnectionString("DefaultConnection"),
            options =>
            {
                options.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                    configuration.GetSection("Schemas:OrderSchema").Value);
            });

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderUoW, UnitOfWork>();

        return services;
    }
}