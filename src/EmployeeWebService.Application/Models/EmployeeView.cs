using EmployeeWebService.Domain.Aggregates;
using EmployeeWebService.Domain.Entities;

namespace EmployeeWebService.Application.Models;

public class EmployeeView
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int? CompanyId { get; set; }
    public Passport Passport { get; set; } = null!;
    public DepartmentView? Department { get; set; }

    public static EmployeeView Convert(EmployeeAggregate employeeAggregate)
        => new EmployeeView
        {
            Id = employeeAggregate.Id,
            Name = employeeAggregate.Name,
            Surname = employeeAggregate.Surname,
            Phone = employeeAggregate.Phone,
            CompanyId = employeeAggregate.CompanyId,
            Department = new DepartmentView()
            {
                Name = employeeAggregate.Department?.Name,
                Phone = employeeAggregate.Department?.Phone
            },
            Passport = new Passport()
            {
                Type = employeeAggregate.Passport.Type,
                Number = employeeAggregate.Passport.Number
            }
        };
}
