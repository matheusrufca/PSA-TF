﻿using Matheus.DAL;
using Matheus.DAL.Models;
using Matheus.Repository.IRepositories;
using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;

namespace Matheus.Repository
{
	public class FuelTypeRepository : BaseRepository<FuelType>, IFuelTypeRepository
	{
		public FuelTypeRepository(IUnitOfWork context) : base(context)
		{
		}
	}
}