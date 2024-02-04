using Dapper;

namespace EmployeeWebService.Data;

public static class DatabaseInitializer
{
    public static void Migrate(DapperContext context)
    {
        var query = """
            CREATE TABLE IF NOT EXISTS  companies(
              id SERIAL PRIMARY KEY,
              name VARCHAR(80) NOT NULL
            );

            CREATE TABLE IF NOT EXISTS  departments(
              id SERIAL PRIMARY KEY,
              name VARCHAR(80) NOT NULL,
              phone VARCHAR(20) UNIQUE CHECK(phone !=''),
              company_id INTEGER REFERENCES companies(id) ON DELETE CASCADE
            );

            CREATE TABLE  IF NOT EXISTS  employees(
              id SERIAL PRIMARY KEY,
              name VARCHAR(20) NOT NULL,
              surname VARCHAR(20) NOT NULL,
              phone VARCHAR(20) UNIQUE CHECK(phone !=''),
              passport_type VARCHAR(20) NOT NULL,
              passport_number VARCHAR(30) NOT NULL,
              department_id INTEGER REFERENCES departments(id) ON DELETE SET NULL
            );
            """;

        using var connection = context.CreateConnection();

        connection.Execute(query);
    }

    public static void SeedData(DapperContext context)
    {
        var query = """
            DO $$
            BEGIN
                INSERT INTO companies (id, name)
                VALUES 
                  (1,'Microsoft'),
                  (2,'Apple'),
                  (3,'SpaceX')
                ON CONFLICT (id) DO NOTHING;

                INSERT INTO public.departments(
                    id, name, phone, company_id)
                    VALUES (1,'IT', '+55 555', 1), 
                            (2,'Managment', '+66 666', 2),
                            (3,'Space', '+7 7766', 3),
                            (4,'Managment', '+78 436', 1)
                ON CONFLICT (id) DO NOTHING;
            END $$;
            """;

        using var connection = context.CreateConnection();

        connection.Execute(query);
    }  
}
