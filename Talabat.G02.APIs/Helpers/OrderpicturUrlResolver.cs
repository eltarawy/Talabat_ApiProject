using AutoMapper;
using AutoMapper.Execution;
using TalabatG02.APIs.Dtos;
using TalabatG02.Core.Entities.OrderAggregtion;

namespace TalabatG02.APIs.Helpers
{
    public class OrderpicturUrlResolver: IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderpicturUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
                return $"{configuration["ApiBaseUrl"]}{source.Product.PictureUrl}";
            return string.Empty;
        }
    }
}
