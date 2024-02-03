using Dapper;
using EmployeeWebService.Domain;
using EmployeeWebService.Domain.Entities;

namespace EmployeeWebService.Data.Repositories;

internal class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository
        (DapperContext context) : base(context) { }

    public async Task<int> CreateEmployeeAsync(Employee employee)
    {
        var query = $"""
                INSERT INTO employees 
                (name, surname, phone, 
                passport_type, 
                passport_number, 
                department_id) 
                VALUES (@Name, @Surname, @Phone, @PassportType, @PassportNumber, @DepartmentId) 
                RETURNING id;
            """;

        var dynamicParams = GetDynamicParamsFromEmployee(employee);
        using var connection = Context.CreateConnection();
        var createdId = await connection.QueryFirstAsync<int>(query, dynamicParams);
        return createdId;
    }

    private DynamicParameters GetDynamicParamsFromEmployee(Employee employee)
    {
        DynamicParameters dynamic = new DynamicParameters();
        dynamic.Add("@Name", employee.Name);
        dynamic.Add("@Surname", employee.Surname);
        dynamic.Add("@Phone", employee.Phone);
        dynamic.Add("@PassportType", employee.Passport.Type);
        dynamic.Add("@PassportNumber", employee.Passport.Number);
        dynamic.Add("@DepartmentId", employee.DepartmentId);
        return dynamic;
    }
}
