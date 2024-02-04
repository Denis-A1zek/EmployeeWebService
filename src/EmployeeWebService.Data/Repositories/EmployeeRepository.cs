using Dapper;
using EmployeeWebService.Application.Models;
using EmployeeWebService.Domain;
using EmployeeWebService.Domain.Aggregates;
using EmployeeWebService.Domain.Entities;
using EmployeeWebService.Domain.Interfaces;

namespace EmployeeWebService.Data.Repositories;

internal class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository
        (DapperContext context) : base(context) { }

    public async Task<int> CreateAsync(Employee employee)
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

        using var connection = Context.CreateConnection();
        var dynamicParams = GetDynamicParamsFromEmployee(employee);
        var createdId = await connection.QueryFirstAsync<int>(query, dynamicParams);
        return createdId;
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = Context.CreateConnection();
     
        var deleteQuery = """
            DELETE FROM employees WHERE id = @id
            """;

        return await connection.ExecuteAsync(deleteQuery, new { id });
    }


    public async Task<IEnumerable<EmployeeAggregate>> GetEmployesByFilter(int? companyId, int? departmentId)
    {
        var query = $@"
            SELECT 
                e.id, 
                e.name, 
                e.surname, 
                e.phone, 
                e.passport_type as {nameof(EmployeeAggregate.Passport.Type)}, 
                e.passport_number as {nameof(EmployeeView.Passport.Number)}, 
                d.name AS {nameof(EmployeeAggregate.Department.Name)}, 
                d.phone AS {nameof(EmployeeAggregate.Department.Phone)}, 
                c.id AS Id
            FROM 
                public.employees e
            JOIN 
                departments d ON d.id = e.department_id
            JOIN 
                companies c ON d.company_id = c.id
            WHERE 
                    (@CompanyId IS NULL OR c.id = @CompanyId)
                    AND (@DepartmentId IS NULL OR d.id = @DepartmentId);
            ";

        using var connection = Context.CreateConnection();

        var employees = await connection.QueryAsync<EmployeeAggregate, Passport, Department, Company, EmployeeAggregate>(
            query,
            map: (employee, passport, department, company) =>
            {
                employee.Passport = passport;
                employee.Department = department;
                employee.CompanyId = company.Id;
                return employee;
            },
            new { companyId, departmentId },
            splitOn: "Type, Name, Id");
        return employees;
    }

    public async Task<IEnumerable<EmployeeAggregate>> GetEmployeesAsync()
    {
        var query = $"""
            SELECT 
                e.id, 
                e.name, 
                e.surname, 
                e.phone, 
                e.passport_type as {nameof(EmployeeAggregate.Passport.Type)}, 
                e.passport_number as {nameof(EmployeeView.Passport.Number)}, 
                d.name AS {nameof(EmployeeAggregate.Department.Name)}, 
                d.phone AS {nameof(EmployeeAggregate.Department.Phone)}, 
                c.id AS Id
            FROM 
                public.employees e
            JOIN 
                departments d ON d.id = e.department_id
            JOIN 
                companies c ON d.company_id = c.id
            """;

        using var connection = Context.CreateConnection();

        var departments = await connection.QueryAsync<EmployeeAggregate, Passport, Department, Company, EmployeeAggregate>(
            query,
            map: (employee, passport, department, company) =>
            {
                employee.Passport = passport;
                employee.Department = department;
                employee.CompanyId = company.Id;
                return employee;
            },
            splitOn: "Type, Name, Id");
        return departments.ToList();
    }


    //TODO
    public async Task<int> UpdateAsync(IRenewableEmployee updatedEmployee)
    {
        var fieldsToUpdate = (new List<string> {
                $"{(updatedEmployee.Name != null ? "name = @Name" : string.Empty)}",
                $"{(updatedEmployee.Surname != null ? "surname = @Surname" : string.Empty)}",
                $"{(updatedEmployee.Phone != null ? "phone = @Phone" : string.Empty)}",
                $"{(updatedEmployee.DepartmentId != null ? "department_id = @DepartmentId" : string.Empty)}",
                $"{(updatedEmployee.Passport?.Number != null ? "passport_number = @PassportNumber" : string.Empty)}",
                $"{(updatedEmployee.Passport?.Type != null ? "passport_type = @PassportType" : string.Empty)}"
            }).Where(f => f.Length > 1);
        
        string updateQuery = $@"
                UPDATE employees
                SET
                    {string.Join(',', fieldsToUpdate)}
                WHERE Id = @Id
        ";

        DynamicParameters dynamic = new DynamicParameters();
        dynamic.Add("@Id", updatedEmployee.Id);
        dynamic.Add("@Name", updatedEmployee.Name);
        dynamic.Add("@Surname", updatedEmployee.Surname);
        dynamic.Add("@Phone", updatedEmployee.Phone);
        dynamic.Add("@PassportType", updatedEmployee.Passport?.Type);
        dynamic.Add("@PassportNumber", updatedEmployee.Passport?.Number);
        dynamic.Add("@DepartmentId", updatedEmployee.DepartmentId);

        using var connection = Context.CreateConnection();
        return await connection.ExecuteAsync(updateQuery, dynamic);
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
