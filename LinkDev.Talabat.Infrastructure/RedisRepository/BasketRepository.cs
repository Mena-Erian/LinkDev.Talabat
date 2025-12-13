using LinkDev.Talabat.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Domain.Entities.Basket;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.RedisRepository
{
    public class BasketRepository(IConnectionMultiplexer redis) : IBasketRepository
    {

        private readonly IDatabase _database = redis.GetDatabase();
        public async Task<Basket?> GetAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket?>(basket!);
        }

        public async Task<Basket?> CreateOrUpdateAsync(Basket basket, TimeSpan expiration)
        {
            var serializedBasket = JsonSerializer.Serialize(basket);
            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, serializedBasket, expiration);

            if (!isCreatedOrUpdated) return null;
            return basket;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }
    }
}
