using AutoMapper;
using TalabatG02.APIs.Dtos;
using TalabatG02.Core.Entities;
using TalabatG02.Core.Entities.OrderAggregtion;

namespace TalabatG02.APIs.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(PD => PD.ProductBrand, O => O.MapFrom(P => P.ProductBrand.Name))
                .ForMember(PD => PD.ProductType, O => O.MapFrom(P => P.ProductType.Name))
                .ForMember(PD => PD.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

         
            CreateMap<CustomerBaskerDto, CustomerBasket>();
            CreateMap<CustomerBaskerDto,BasketItem>();
            CreateMap<TalabatG02.Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto,TalabatG02.Core.Entities.OrderAggregtion.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(M => M.DeliveryMethod.ShortName))
                .ForMember(D => D.DeliveryMethodCost, O => O.MapFrom(M => M.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(D => D.ProductId, O => O.MapFrom(m => m.Product.ProductId))
                .ForMember(D => D.ProductName, O => O.MapFrom(m => m.Product.ProductName))
                .ForMember(D => D.PictureUrl, O => O.MapFrom(m => m.Product.PictureUrl))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<OrderpicturUrlResolver>());

        }
    }
}  
     