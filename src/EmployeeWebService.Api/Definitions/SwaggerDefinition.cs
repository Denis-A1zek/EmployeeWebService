using Asp.Versioning;
using System.Reflection;

namespace EmployeeWebService.Api;

public static class SwaggerDefinition
{
    /// <summary>
    /// Подключение Swagger и его настройка
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwagger(this IServiceCollection services)
    {
        services
            .AddApiVersioning()
            .AddApiExplorer(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Version = "v1",
                Title = "Employee.Api",
                Description = "Веб-сервис для работы с сотрудниками"
            });

            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });   
    }


    /// <summary>
    /// Подключение Swagger в контейнер обработки
    /// </summary>
    /// <param name="app"></param>
    public static void UseSwaggerDef(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(conf =>
            {
                conf.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee.Api v1.0");
            });
        }
    }
}
