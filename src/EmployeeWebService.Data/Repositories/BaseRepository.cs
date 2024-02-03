namespace EmployeeWebService.Data.Repositories;

public abstract class BaseRepository
{
    public BaseRepository(DapperContext context)
    {
        Context = context;
    }
    protected readonly DapperContext Context;

}
