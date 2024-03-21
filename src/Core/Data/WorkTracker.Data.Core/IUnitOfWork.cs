namespace WorkTracker.Data.Core
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}