using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeApp.Web.Services
{
    public class RedisService
    {
        private readonly string _redisHost;
        private readonly string _redisPort;
        private IDatabase database;
        private ConnectionMultiplexer _redis;
        public RedisService(IConfiguration configuration)
        {
            _redisHost = configuration["Redis:Host"];
            _redisPort = configuration["Redis:Port"];
        }

        public void Connect()
        {
            var config = $"{_redisHost}:{_redisPort}";
            _redis = ConnectionMultiplexer.Connect(config);
 
        }

        public IDatabase GetDb(int db)
        {
           return _redis.GetDatabase(db);
        }
    }
}
