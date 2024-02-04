using EmployeeWebService.Application.Services;
using EmployeeWebService.Application.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeWebService.Application;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();

        services.AddValidatorsFromAssembly(typeof(EmployeeModelValidation).Assembly);

        return services;
    }
}
