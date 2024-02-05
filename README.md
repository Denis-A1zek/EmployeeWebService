# Employee API

Веб-сервис сотрудников
![изображение](https://github.com/Denis-A1zek/EmployeeWebService/assets/130150382/e9d6d647-abee-4247-9ca8-8c95dfacfa7c)

# Запуск
Миграция базы данных и добавление тестовых данных происходит автоматически при первом запуске.
Для отключения этой возможности необходимо удалить код в проекте EmployeWebService.Api в основном классе Program
~~~ C#
try
{
    var context = app.Services.GetRequiredService<DapperContext>();
    DatabaseInitializer.Migrate(context);
    DatabaseInitializer.SeedData(context);
}
catch (Exception)
{
    app.Services.
        GetRequiredService<ILogger<Program>>()
        .LogError("Ошибка при инициализации базы данных");
}
~~~

ВАЖНО: В файле appsettings.json заменить строку подключения к БД на вашу
~~~ json
"ConnectionStrings": {
  "SqlConnection": "Server=server_ip;Database=employee;Username=USERNAME;Password=PASSWORD"
}
~~~
## Docker Support
1. Зайти в корневую папку проекта и написать
   ~~~
   2. docker compose up
   ~~~
## Локально
1. Установить зависимости PostgreSQL 16.1 и .NET 8.
2. Запустить приложения.

# Технологии

- .NET 8
- ASP .NET Core Web API
- PostgreSQL

### Библиотеки

- Dapper
- Npgsql
- FluentValidation 

# Endpoints

|Метод| Endpoint (Вспомогательные) | Описание |
|---| ------------- | ------------- |
|GET| /api/v1/companies  | Получить список всех компаний   |
|GET| /api/v1/departments  | Получить список всех отделов |

|Метод| Endpoint (Основные) | Описание |
|----| ------------- | ------------- |
|GET| /api/v1/employees |Получить список всех сотрудников (в случае если ни один из параметров <company_id и department_id> не указан) |
| | /api/v1/employees?company_id=1 | Получить всех сотрудников у которых id компании равен 1 |
| | /api/v1/employees?department_id=2 | Получить всех сотрудников у которых id отдела равен 2 |
| | /api/v1/employees?company_id=1&department_id=2 | Комбинируемая версия |
|POST| /api/v1/employees |Добавляет нового сотрудника |
|DELETE| /api/v1/employees/1 |Удалить сотрудника с id равным 1 |
|GET| /api/v1/employees/1 |Получить сотрудника с id равным 1|
|PATCH| /api/v1/employees/1 |Частично изменить сотрудника с id равным 1. **Будьте внимательны с обновляемыми полями, указывайте только нужные**|

# Пример запроса в Swagger
![изображение](https://github.com/Denis-A1zek/EmployeeWebService/assets/130150382/3758157e-b9dd-49ba-8afd-28dd8fc7f71d)
