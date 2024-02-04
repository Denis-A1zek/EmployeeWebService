using EmployeeWebService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeWebService.Application;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();

        return services;
    }
}
