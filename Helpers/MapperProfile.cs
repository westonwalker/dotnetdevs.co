using AutoMapper;
using dotnetdevs.Models;
using dotnetdevs.ViewModels;

namespace dotnetdevs.Helpers
{
	public class MapperProfile : Profile
	{
		public MapperProfile() 
		{
			CreateMap<DeveloperCreate, Developer>();
			CreateMap<Developer, DeveloperEdit>();
			CreateMap<DeveloperEdit, Developer>();
			CreateMap<CompanyCreate, Company>();
			CreateMap<CompanyEdit, Company>();
			CreateMap<Company, CompanyEdit>();
		}
	}
}
