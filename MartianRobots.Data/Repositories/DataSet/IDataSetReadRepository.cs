using MartianRobots.Data.Entities;

namespace MartianRobots.Data.Repositories
{
    public interface IDataSetReadRepository
    {
        IEnumerable<DataSet> GetAllSets();
        string GetSetNameByRunId(Guid ruinId);
    }
}
