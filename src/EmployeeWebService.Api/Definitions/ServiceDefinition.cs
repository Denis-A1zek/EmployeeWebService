using EmployeeWebService.Data;
using EmployeeWebService.Application;

namespace EmployeeWebService.Api.Definitions;

public static class ServiceDefinition
{
    /// <summary>
    /// Конфигурация сервисов
    /// </summary>
    /// <param name="services"></param>
    public static void AddServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddSwagger();

        services.AddData();
        services.AddCore();
    }
}
