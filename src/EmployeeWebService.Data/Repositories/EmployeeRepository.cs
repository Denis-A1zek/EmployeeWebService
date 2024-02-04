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


    public async Task<IEnumerable<EmployeeAggregate>> GetEmployesByFilter
        (int? companyId, int? departmentId)
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

    public async Task<int> UpdateAsync(IRenewableEmployeeField updatedEmployee)
    {
        var fieldsToUpdate = (new List<string> {
                AddField("name", updatedEmployee.Changes.Name),
                AddField("surname", updatedEmployee.Changes.Surname),
                AddField("phone", updatedEmployee.Changes.Phone),
                AddField("department_id", updatedEmployee.Changes.DepartmentId),
                AddField("passport_number", updatedEmployee.Changes.PassportNumber),
                AddField("passport_type", updatedEmployee.Changes.PassportType)
            }).Where(f => f.Length > 1);
        
        string updateQuery = $@"
                UPDATE employees
                SET
                    {string.Join(',', fieldsToUpdate)}
                WHERE Id = @Id
        ";

        DynamicParameters dynamic = new DynamicParameters();
        dynamic.Add("@id", updatedEmployee.EmployeeId);
        dynamic.Add("@name", updatedEmployee.Changes.Name);
        dynamic.Add("@surname", updatedEmployee.Changes.Surname);
        dynamic.Add("@phone", updatedEmployee.Changes.Phone);
        dynamic.Add("@passport_type", updatedEmployee.Changes.PassportType);
        dynamic.Add("@passport_number", updatedEmployee.Changes.PassportNumber);
        dynamic.Add("@department_id", updatedEmployee.Changes.DepartmentId);

        using var connection = Context.CreateConnection();
        return await connection.ExecuteAsync(updateQuery, dynamic);
    }



    private string AddField(string fieldName, object? value)
    {
        return value != null ? $"{fieldName} = @{fieldName}" : string.Empty;
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

    public async Task<EmployeeAggregate?> GetById(int id)
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
            WHERE e.id = @id
            """;

        using var connection = Context.CreateConnection();

        var employee = await connection.QueryAsync<EmployeeAggregate, Passport, Department, Company, EmployeeAggregate>(
            query,
            map: (employee, passport, department, company) =>
            {
                employee.Passport = passport;
                employee.Department = department;
                employee.CompanyId = company.Id;
                return employee;
            },
            param: new { id },
            splitOn: "Type, Name, Id");
        return employee.FirstOrDefault();
    }
}
