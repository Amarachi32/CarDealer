using AutoMapper;
using Car.BLLayer.DTO.RequestDtos;
using Car.BLLayer.DTO.ResponseDto;
using Car.DLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.DTO.Mapping
{
    public class MappingProfile : Profile
    {
        protected MappingProfile()
        {
            CreateMap<Product, ProductResponseDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(t => t.ProductType, o => o.MapFrom(s => s.ProductType.Name));
             //   .ForMember(t => t.PictureUrl, o => o.MapFrom<>);
             //below address is from identity
          //  CreateMap<identity.Address, AddressDto>().ReverseMap();
            CreateMap<CartDto, ShoppingCart>();
            CreateMap<ShoppingCartItemDto,  ShoppingCartItem>();
            CreateMap<AddressDto, Car.DLL.Entities.Address>();
            CreateMap<Order, OrderReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.Id))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.ProductUrl));
        }

        
    }
}
