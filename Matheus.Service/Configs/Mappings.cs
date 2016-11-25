﻿using AutoMapper;
using Matheus.DAL.Models;
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
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id));

			CreateMap<CarModel, Car>()
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id));

			CreateMap<Car, CarFormViewModel>();
			CreateMap<CarFormViewModel, Car>();
			
			CreateMap<CreateCarViewModel, Car>()
				.ForMember(dest => dest.Odometer, opt => opt.ResolveUsing(src => new Odometer()));
				//.ForMember(d => d.Odometer, o => o.NullSubstitute(new Odometer()));

			CreateMap<EditCarViewModel, Car>()
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id));


			CreateMap<Odometer, OdometerModel>();
			CreateMap<OdometerModel, Odometer>();

			//CreateMap<FuelSupply, FuelSupplyModel>();
			//CreateMap<FuelSupplyModel, FuelSupply>();
		}
	}


	public class FuelSupplyProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<FuelSupply, FuelSupplyModel>();
			CreateMap<FuelSupplyModel, FuelSupply>();


			CreateMap<AddFuelSupplyViewModel, FuelSupply>()
				.ForMember(dest => dest.FuelPrice, opts => opts.MapFrom(src => src.FuelType.Price));




			CreateMap<FuelType, FuelTypeModel>();
			CreateMap<FuelTypeModel, FuelType>();
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