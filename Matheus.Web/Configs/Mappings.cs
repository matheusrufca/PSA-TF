using AutoMapper;
using Matheus.Data;
using Web.Models;

namespace Matheus.Web.Configs.Mappings
{
	public class AutoMapperConfig
	{
		public static void Register()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.AddProfile(new CarProfile());
				cfg.AddProfile(new FuelSupplyProfile());
				cfg.AddProfile(new FuelTypeProfile());
			});
		}

	}

	public class CarProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<Car, CarModel>()
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.CarId));

			CreateMap<CarModel, Car>()
				.ForMember(dest => dest.CarId, opts => opts.MapFrom(src => src.Id));

			CreateMap<Car, CarFormViewModel>();
			CreateMap<CarFormViewModel, Car>();

			CreateMap<Car, CreateCarViewModel>();
			CreateMap<CreateCarViewModel, Car>();


			CreateMap<Car, EditCarViewModel>()
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.CarId));
			CreateMap<EditCarViewModel, Car>()
				.ForMember(dest => dest.CarId, opts => opts.MapFrom(src => src.Id));


			CreateMap<Odometer, OdometerModel>();
			CreateMap<OdometerModel, Odometer>();
		}
	}


	public class FuelSupplyProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<FuelSupply, FuelSupplyModel>();
			CreateMap<FuelSupplyModel, FuelSupply>();
		}
	}

	public class FuelTypeProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<FuelType, FuelTypeModel>();
			CreateMap<FuelTypeModel, FuelType>();
		}
	}
}