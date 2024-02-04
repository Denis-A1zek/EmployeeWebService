using Dapper;
using EmployeeWebService.Domain;
using EmployeeWebService.Domain.Aggregates;
using EmployeeWebService.Domain.Entities;

namespace EmployeeWebService.Data.Repositories;

public class DepartmentRepository : BaseRepository, IDepartmentRepository
{
    public DepartmentRepository
        (DapperContext context) : base(context) { }

    public async Task<Department> GetDepartmentByIdAsync(int id)
    {
        var query = """
            SELECT * FROM departments WHERE id = @Id
            """;
        
        using var connection = Context.CreateConnection();
        var department = await connection.QueryFirstOrDefaultAsync<Department>(query, new { id });
        return department;
    }

    public async Task<IReadOnlyCollection<DepartmentAggregate>> GetDepartmentsAsync()
    {
        var query = $"""
                SELECT 
                departments.id, 
                departments.name, 
                phone, 
                company_id AS {nameof(DepartmentAggregate.Company.Id)}, 
                companies.name AS {nameof(DepartmentAggregate.Company.Name)}
                FROM public.departments
                JOIN companies ON departments.company_id = companies.id
            """;
        using var connection = Context.CreateConnection();

        var departments = await connection
            .QueryAsync<DepartmentAggregate, Company, DepartmentAggregate>(
                query,
                (department, company) =>
                {
                    department.Company = company;
                    return department;
                },
                splitOn: nameof(DepartmentAggregate.Company.Id));
        return departments.ToList();
    }
}
