using EmployeeWebService.Application.Models;
using FluentValidation;

namespace EmployeeWebService.Application.Validations;

public class EmployeeFieldChangesValidation : AbstractValidator<EmployeeFieldChanges>
{
    public EmployeeFieldChangesValidation()
    {
        RuleFor(e => e.Name)
            .MaximumLength(20).WithMessage("Длина не может превышать 20 символов.");
        RuleFor(e => e.Surname)
            .MaximumLength(20).WithMessage("Длина не может превышать 20 символов.");
        RuleFor(e => e.Phone)
            .Matches(@"^\+(?:[0-9] ?){6,14}[0-9]$").WithMessage("Неверный формат номера телефона.");
        RuleFor(e => e.PassportType)
            .MaximumLength(20).WithMessage("Длина не может превышать 20 символов.");
        RuleFor(e => e.PassportNumber)
            .MaximumLength(20).WithMessage("Длина не может превышать 20 символов.");
    }
}
