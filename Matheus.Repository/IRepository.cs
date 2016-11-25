using Matheus.Data;
using System;
using System.Collections.Generic;

namespace Matheus.Repository
{
	public interface IRepository<TEntity>: IDisposable
	{
		TEntity Get(int id);
		IEnumerable<TEntity> Get();
		TEntity Add(TEntity item);
		TEntity Edit(int id, TEntity item);
		void Remove(int id);
		bool Contains(int id);
	}


	public interface ICarRepository : IRepository<Car>
{
		Car AddFuelSupply(int carId, FuelSupply item);
	}

	public interface IFuelSupplyRepository : IRepository<FuelSupply>
	{
		Car AddFuelSupply(int id, FuelSupply item);
	}
}
