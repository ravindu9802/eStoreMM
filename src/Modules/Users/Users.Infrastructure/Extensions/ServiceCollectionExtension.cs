using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Abstractions;
using Users.Domain.Repositories;
using Users.Infrastructure.Authentication;
using Users.Infrastructure.Persistance;
using Users.Infrastructure.Repositories;

namespace Users.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUserInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddNpgsql<UserDbContext>(configuration.GetConnectionString("DefaultConnection"),
            options =>
            {
                options.MigrationsHistoryTable(HistoryRepository.DefaultTableName,
                    configuration.GetSection("Schemas:UserSchema").Value);
            });
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserUoW, UnitOfWork>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
        return services;
    }
}