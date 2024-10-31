using AutoMapper;
using WebApplication7_petPals.Models;
using WebApplication7_petPals.Models.Dto.CatogoryDto;
using WebApplication7_petPals.Models.Dto.ProductDto;
using WebApplication7_petPals.Models.Dto.UserDto;
using WebApplication7_petPals.Models.Dto.WishlistDto;


namespace WebApplication7_petPals.Mapping
{
    public class AppMapper:Profile
    {
        public AppMapper()
        {
            CreateMap<User,UserRegisterDto>().ReverseMap();
            CreateMap<User, OutputUserDto>().ReverseMap();
            CreateMap<Category,CreateCatogoryDto>().ReverseMap();
            CreateMap<Category, OutCatogryDto>().ReverseMap();
            CreateMap<Product,CreatePrdctDto>().ReverseMap();
            CreateMap<Wishlists,WishlistDto>().ReverseMap();


        }
    }
}
