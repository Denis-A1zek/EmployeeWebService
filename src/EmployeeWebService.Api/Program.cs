using EmployeeWebService.Api.Definitions;
using EmployeeWebService.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();

var app = builder.Build();
app.UseMiddlewares();

try
{
    var context = app.Services.GetRequiredService<DapperContext>();
    DatabaseInitializer.Migrate(context);
    DatabaseInitializer.SeedData(context);
}
catch (Exception)
{
    app.Services.
        GetRequiredService<ILogger<Program>>()
        .LogError("Ошибка при инициализации базы данных");
}

app.Run();
