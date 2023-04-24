using AutoMapper;
using OganiShop.Entities;
using OganiShop.Models;

namespace OganiShop.Helpers
{
	public class ApplicationMapper : Profile
	{
        public ApplicationMapper()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<ShippingAddress, ShippingAddressModel>().ReverseMap();
            CreateMap<ContactMessage, ContactMessageModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<CategoryBlog, CategoryBlogModel>().ReverseMap();
            CreateMap<Tag, TagModel>().ReverseMap();

        }
    }
}
