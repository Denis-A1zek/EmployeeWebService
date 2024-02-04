using EmployeeWebService.Domain.Entities;

namespace EmployeeWebService.Application.Models;

public class EmployeeModel
{
    public string Name { get; set; } 
    public string Surname { get; set; } 
    public string Phone { get; set; }
    public int DepartmentId { get; set; }
    public string PassportType { get; set; }
    public string PassportNumber { get; set; }

    internal Employee ToEmployee()
        => new Employee()
        {
            Name = Name,
            Surname = Surname,
            Phone = Phone,
            DepartmentId = DepartmentId,
            Passport = new Passport()
            {
                Number = PassportNumber,
                Type = PassportType
            }
        };
}
