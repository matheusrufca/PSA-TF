﻿using Matheus.DAL;
using Matheus.DAL.Models;
using Matheus.Repository.IRepositories;

namespace Matheus.Repository
{
	public class FuelSupplyRepository : BaseRepository<FuelSupply>, IFuelSupplyRepository
	{
		public FuelSupplyRepository(IUnitOfWork context) : base(context)
		{
		}
	}
}