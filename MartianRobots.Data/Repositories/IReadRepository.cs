namespace MartianRobots.Data.Repositories
{
    public interface IReadRepository<T>
    {
        IQueryable<T> GetAll();
    }
}
