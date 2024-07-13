using StackExchange.Redis;

namespace GameOfLife.Domain.Contexts
{
    public class RedisContext
    {

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        static RedisContext()
        {
            RedisContext.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                string connection = Environment.GetEnvironmentVariable("RedisConnection", EnvironmentVariableTarget.Process);
                return ConnectionMultiplexer.Connect(connection);
            });
        }
    }
}