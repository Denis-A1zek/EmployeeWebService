namespace EmployeeWebService.Api.Definitions;

public static class MiddlewareDefinition
{
    public static void UseMiddlewares(this WebApplication app)
    {
        app.UseSwaggerDef();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseExceptionHandler();
        app.MapControllers();
    }

}
