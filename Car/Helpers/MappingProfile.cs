using AutoMapper;
using Car.BLLayer.DTO.RequestDtos;
using Car.BLLayer.DTO.ResponseDto;
using Car.DLL.Entities;

namespace Car.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(t => t.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(dest => dest.ImageFile, opt => opt.MapFrom(src => src.PictureUrl)).ReverseMap();
            //   .ForMember(t => t.PictureUrl, o => o.MapFrom<>);

            CreateMap<Product, ProductResponseDto>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(t => t.ProductType, o => o.MapFrom(s => s.ProductType.Name));


            //below address is from identity
            //  CreateMap<identity.Address, AddressDto>().ReverseMap();
            CreateMap<CartDto, ShoppingCart>();
            CreateMap<ShoppingCartItemDto, ShoppingCartItem>();
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
