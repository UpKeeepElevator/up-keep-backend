using UpKeep.Data.Context;

namespace UpKeep.Data;

public class RepositorioBase
{
    protected readonly UpKeepDbContext dbContext;

    public RepositorioBase(UpKeepDbContext mySqlContext)
    {
        dbContext = mySqlContext;
    }

    protected void SavesChanges()
    {
        dbContext.SaveChanges();
        // _transaction.Commit();
        // Database().Close();
    }

    protected void UndoChanges()
    {
        // _transaction.Rollback();
        // Database().Close();
    }
}