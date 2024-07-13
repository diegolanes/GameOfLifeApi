using GameOfLife.Domain.Contexts;
using GameOfLife.Domain.Interfaces;
using StackExchange.Redis;

namespace GameOfLife.Domain.Services
{
    public class RedisService : IRedisService
    {
        public void SetKeyValue(Guid key, object obj)
        {
            IDatabase cache = RedisContext.Connection.GetDatabase();

            {
                cache.StringSet($"KeyValue:{key}", (RedisValue)obj);

                // https://redis.io/commands/expire
                // Set all keys to not live for more than 24 hours
                cache.KeyExpire($"KeyValue:{key}", new TimeSpan(23, 59, 59));
            }
        }

        public object GetKeyValue(Guid key)
        {
            IDatabase cache = RedisContext.Connection.GetDatabase();

            return cache.StringGet($"KeyValue:{key}");
        }
    }
}

