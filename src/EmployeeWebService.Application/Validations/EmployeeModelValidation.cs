using EmployeeWebService.Application.Models;
using FluentValidation;

namespace EmployeeWebService.Application.Validations;

public class EmployeeModelValidation : AbstractValidator<EmployeeModel>
{
    public EmployeeModelValidation()
    {
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Имя сотрудника не может быть пустым.")
            .MaximumLength(20).WithMessage("Длина не может превышать 20 символов.");
        RuleFor(e => e.Surname)
            .NotEmpty().WithMessage("Фамилия сотрудника не может быть пустой.")
            .MaximumLength(20).WithMessage("Длина не может превышать 20 символов.");
        RuleFor(e => e.Phone)
            .NotEmpty().WithMessage("Номер телефона не может быть пустым.")
            .Matches(@"^\+(?:[0-9] ?){6,14}[0-9]$").WithMessage("Неверный формат номера телефона.");
        RuleFor(e => e.DepartmentId)
            .NotEmpty().WithMessage("Номер отделения не может быть пустим.");
        RuleFor(e => e.PassportType)
            .NotEmpty().WithMessage("Тип паспорта не может быть пустым.")
            .MaximumLength(20).WithMessage("Длина не может превышать 20 символов.");
        RuleFor(e => e.PassportNumber)
            .NotEmpty().WithMessage("Номер паспорта не может быть пустым.")
            .MaximumLength(20).WithMessage("Длина не может превышать 20 символов.");
    }
}
