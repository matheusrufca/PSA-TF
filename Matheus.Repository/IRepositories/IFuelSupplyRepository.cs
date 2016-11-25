using System;
using System.Collections.Generic;
using Matheus.DAL.Models;

namespace Matheus.Repository.IRepositories
{
	public interface IFuelSupplyRepository: IDisposable
	{
		FuelSupply GetById(int id);

		IEnumerable<FuelSupply> Get();
	}
}