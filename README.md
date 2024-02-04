# Employee API

Веб-сервис сотрудников

# Запуск
## Docker Support
1. Изменить appsettings.json. Строку подключения к базе даненых.
~~~ json
"ConnectionStrings": {
  "SqlConnection": "Server=server_ip;Database=employee;Username=USERNAME;Password=PASSWORD"
}
~~~
3. Зайти в корневую папку проекта и написать
   ~~~
   2. docker compose up
   ~~~
4. Миграция базы данных произойдёте автоматически при первом запуске приложения.
5. Если не была изменена переменная "ASPNETCORE_ENVIRONMENT=Development" в файле dockercompose.yml, то можно протестировать приложение при помощи Swagger

## Локально
1. Установить зависимости PostgreSQL и .NET 8+.
2. Изменить appsettings.json. Строку подключения к базе даненых.
~~~ json
"ConnectionStrings": {
  "SqlConnection": "Server=server_ip;Database=employee;Username=USERNAME;Password=PASSWORD"
}
~~~
4. Запустить приложения.
5. Миграция базы данных произойдёте автоматически при первом запуске приложения.

# Технологии

- .NET 8
- ASP .NET Core Web API
- PostgreSQL

## Библиотеки

- Dapper
- Npgsql.EntityFrameworkCore.PostgreSQL
- FluentValidation 

# Endpoints
~~~

~~~
