using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Baskets;
using LinkDev.Talabat.Application.Abstraction.Services.Baskets;
using LinkDev.Talabat.Application.Exceptions;
using LinkDev.Talabat.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Domain.Entities.Basket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Services.Baskets
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper, IConfiguration configuration) : IBasketService
    {
        public async Task<BasketDto?> CreateOrUpdateCustomerBasketAsync(BasketDto basketDto)
        {
            var mappedBasket = mapper.Map<Basket>(basketDto);

            var timeToLive = int.Parse(configuration.GetSection("RedisSetting")["TimeToLiveInDays"] ?? "30");

            var updated = await basketRepository.CreateOrUpdateAsync(mappedBasket, TimeSpan.FromDays(timeToLive));

            if (updated is null) throw new BadRequestException("Failed to create or update the basket. Please try again later.");

            var updatedDto = mapper.Map<BasketDto>(updated);

            return updatedDto;
        }

        public async Task<bool> DeleteCustomerBasketAsync(string id)
        {
            return await basketRepository.DeleteAsync(id);
        }

        public async Task<BasketDto?> GetCustomerBasketAsync(string id)
        {
            var basket = await basketRepository.GetAsync(id);

            if (basket is null) throw new NotFoundException(nameof(Basket), id);

            var mappedBasket = mapper.Map<BasketDto>(basket);

            return mappedBasket;
        }
    }
}
