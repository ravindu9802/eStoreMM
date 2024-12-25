using Microsoft.EntityFrameworkCore;
using Orders.Infrastructure.Persistance;
using Products.Infrastructure.Persistance;
using Users.Infrastructure.Persistance;

namespace eStore.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigrations(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();

        using var userDbContext = scope.ServiceProvider.GetService<UserDbContext>()!;
        userDbContext.Database.Migrate();

        using var productDbContext = scope.ServiceProvider.GetService<ProductDbContext>()!;
        productDbContext.Database.Migrate();

        using var orderDbContext = scope.ServiceProvider.GetService<OrderDbContext>()!;
        orderDbContext.Database.Migrate();
    }
}