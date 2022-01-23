using Core.Application.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class RedisRepository: IRedisRepository
    {
        private readonly IDistributedCache _redisCache;

        public RedisRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task AddUser(User data)
        {
            await _redisCache.SetStringAsync(data.UserId.ToString(), JsonConvert.SerializeObject(data));
        }
        public async Task AddOrder(Order data)
        {
            await _redisCache.SetStringAsync(data.OrderID.ToString(), JsonConvert.SerializeObject(data));
        }

    }
}
