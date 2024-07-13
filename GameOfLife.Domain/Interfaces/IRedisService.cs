namespace GameOfLife.Domain.Interfaces
{
    public interface IRedisService
    {
        public void SetKeyValue(Guid key, object obj);
        public object GetKeyValue(Guid key);
    }
}
