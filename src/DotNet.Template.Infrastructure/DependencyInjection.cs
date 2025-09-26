using DotNet.Template.Domain.Interfaces;
using DotNet.Template.Infrastructure.Data;
using DotNet.Template.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet.Template.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Database configuration - In-Memory database
        services.AddDbContext<TodoDbContext>(options =>
            options.UseInMemoryDatabase("TodoDb"));

        // Repository registration
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
