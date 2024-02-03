using EmployeeWebService.Data.Repositories;
using EmployeeWebService.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeWebService.Data;

public static class Extensions
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}
