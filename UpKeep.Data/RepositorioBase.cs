using UpKeep.Data.Context;

namespace UpKeep.Data;

public class RepositorioBase
{
    private readonly UpKeepDbContext _mysqlContext;

    public RepositorioBase(UpKeepDbContext mySqlContext)
    {
        _mysqlContext = mySqlContext;
    }

    protected void SavesChanges()
    {
        // _transaction.Commit();
        // Database().Close();
    }

    protected void UndoChanges()
    {
        // _transaction.Rollback();
        // Database().Close();
    }
}