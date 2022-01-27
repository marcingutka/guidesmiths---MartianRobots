namespace MartianRobots.Analitics.Mappers
{
    public interface IMapper<T1, T2>
    {
        public T2 Map(T1 obj);
    }
}
