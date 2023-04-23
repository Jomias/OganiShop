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

        }
    }
}
