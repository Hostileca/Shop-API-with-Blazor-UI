using AutoMapper;
using Shop.BL.Dtos.Attribute;
using Shop.BL.Dtos.BuyerCard;
using Shop.BL.Dtos.CartElement;
using Shop.BL.Dtos.Category;
using Shop.BL.Dtos.Manufacturer;
using Shop.BL.Dtos.Order;
using Shop.BL.Dtos.Price;
using Shop.BL.Dtos.Product;
using Shop.BL.Dtos.Review;
using Shop.BL.Dtos.User;
using Shop.DAL.Models;
using Shop.DAL.Models.Files;
using Attribute = Shop.DAL.Models.Attribute;

namespace Shop.BL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductAttribute, ProductAttributeReadDto>()
                .ForMember(attributeReadDto => attributeReadDto.Name,
                    opt => opt.MapFrom(productAttribute => productAttribute.Attribute.Name))
                .ForMember(attributeReadDto => attributeReadDto.Value,
                    opt => opt.MapFrom(productAttribute => productAttribute.Value))
                .ForMember(attributeReadDto => attributeReadDto.ProductId,
                    opt => opt.MapFrom(productAttribute => productAttribute.ProductId))
                .ForMember(attributeReadDto => attributeReadDto.AttributeId,
                    opt => opt.MapFrom(productAttribute => productAttribute.AttributeId));
            CreateMap<AttributeUpdateDto, Attribute>();
            CreateMap<ProductAttributeUpdateDto, ProductAttribute>();
            CreateMap<AttributeCreateDto, Attribute>();
            CreateMap<Attribute, AttributeReadDto>();

            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductDetailedReadDto>()
                .ForMember(productDetailedReadDto => productDetailedReadDto.Attributes,
                    opt => opt.MapFrom(src => src.ProductAttributes))
                .ForMember(productDetailedReadDto => productDetailedReadDto.ProductImageIds,
                    opt => opt.MapFrom(src => src.Images));

            CreateMap<Product, ProductReadDto>()
                .ForMember(productReadDto => productReadDto.ProductImageIds,
                    opt => opt.MapFrom(src => src.Images))
                .ForMember(productReadDto => productReadDto.Price,
                    opt => opt.MapFrom(src => src.PriceHistory.FirstOrDefault(p => p.EndDate == null).PriceValue));

            CreateMap<ProductUpdateDto, Product>();

            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryDetailedReadDto>()
                .ForMember(categoryDetailedReadDto => categoryDetailedReadDto.CategoryImageId,
                    opt => opt.MapFrom(src => src.Image));

            CreateMap<Category, CategoryReadDto>()
                .ForMember(categoryReadDto => categoryReadDto.CategoryImageId,
                    opt => opt.MapFrom(src => src.Image));

            CreateMap<ManufacturerCreateDto, Manufacturer>();
            CreateMap<ManufacturerUpdateDto, Manufacturer>();
            CreateMap<Manufacturer, ManufacturerReadDto>()
                .ForMember(manufacturerReadDto => manufacturerReadDto.ManufacturerImageId,
                    opt => opt.MapFrom(src => src.Image));

            CreateMap<Manufacturer, ManufacturerDetailedReadDto>()
                .ForMember(categoryDetailedReadDto => categoryDetailedReadDto.ManufacturerImageId,
                    opt => opt.MapFrom(src => src.Image));

            CreateMap<Order, OrderReadDto>()
                .ForMember(orderReadDto => orderReadDto.Products,
                    opt => opt.MapFrom(src => src.OrderProduct));
            CreateMap<OrderProduct, OrderProductReadDto>();

            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserReadDto>();

            CreateMap<ReviewCreateDto, Review>();
            CreateMap<Review, ReviewReadDto>();
            CreateMap<ReviewUpdateDto, Review>();

            CreateMap<FileBase, int>()
                .ConstructUsing(src => src.Id);

            CreateMap<PriceCreateDto, Price>();
            CreateMap<Price, PriceReadDto>();
            CreateMap<PriceUpdateDto, Price>();

            CreateMap<CartElementCreateDto, CartElement>();
            CreateMap<CartElement, CartElementReadDto>();

            CreateMap<OrderProduct, Product>()
                .ConstructUsing(src => src.Product);

            CreateMap<BuyerCardCreateDto, BuyerCard>();
            CreateMap<BuyerCard, BuyerCardReadDto>();

        }
    }
}
