using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Domain.Repositories;
using Products.Infrastructure.Persistance;
using Products.Infrastructure.Repositories;

namespace Products.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddProductsInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddNpgsql<ProductDbContext>(configuration.GetConnectionString("DefaultConnection"),
            options =>
            {
                options.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                    configuration.GetSection("Schemas:ProductSchema").Value);
            });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductUoW, UnitOfWork>();

        return services;
    }
}