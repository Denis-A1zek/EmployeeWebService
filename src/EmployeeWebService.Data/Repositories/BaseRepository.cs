using Dapper;
using EmployeeWebService.Domain.Entities;

namespace EmployeeWebService.Data.Repositories;

public abstract class BaseRepository
{
    public BaseRepository(DapperContext context)
    {
        Context = context;
    }
    protected readonly DapperContext Context;

    public async Task<bool> IsExsist(string tableName, int entityId)
    {
        var query = $"""
            SELECT id FROM {tableName} WHERE id = @id
            """;

        using var connection = Context.CreateConnection();
        var rows = await connection.ExecuteScalarAsync<int>(query, new { id = entityId });
        return rows == entityId ;
    }
}
