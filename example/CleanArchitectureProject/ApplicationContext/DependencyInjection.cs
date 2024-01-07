using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationContext;

/// <summary>
/// Регистрация зависимостей.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Зарегистрировать контекст работы с БД.
    /// </summary>
    /// <param name="services"> Коллекция сервисов приложения.</param>
    public static IServiceCollection AddApplicationDataContext(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(cnf => 
            cnf.UseInMemoryDatabase("test_db")
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IDataContext, DataContext>();
        return services;
    }
}