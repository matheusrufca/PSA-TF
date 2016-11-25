using Matheus.DAL;
using Matheus.DAL.Models;
using Matheus.Repository.IRepositories;
using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;

namespace Matheus.Repository
{
	public class CarRepository : BaseRepository<Car>, ICarRepository
	{
		public CarRepository(IUnitOfWork context) : base(context)
		{
		}

		public virtual Car Get(int id)
		{
			Car item = null;

			try
			{
				item = _entitySet
					.Where(x => x.Id == id)
					.Include(x => Enumerable.Select<FuelSupply, FuelType>(x.FuelSupplies, y => y.FuelType))
					.FirstOrDefault();

			}
			catch (Exception ex)
			{
				throw ex;
			}

			return item;
		}

		public Car AddFuelSupply(int id, FuelSupply fuelSupply)
		{
			var item = _entitySet.Find(id);
			var fuelType = _context.Set<FuelType>().Find(fuelSupply.FuelType.Id);

			if (item == null)
				throw new ObjectNotFoundException("Car not found.");

			if (fuelType == null)
				throw new ObjectNotFoundException("Fuel type not found.");

			if (fuelSupply == null)
				throw new ArgumentNullException("Fuel supply cannot be null.");


			if (fuelSupply.IsNewSerie)
			{
				item.Odometer.TotalDistance += item.Odometer.CurrentDistance;
				item.Odometer.CurrentDistance = 0;
			}

			fuelSupply.FuelPrice = fuelType.Price;
			fuelSupply.TotalPrice = fuelSupply.FuelQuantity * fuelType.Price;

			fuelSupply.FuelType = fuelType;
			fuelSupply.FueledAt = DateTime.Now;

			try
			{
				item.FuelSupplies.Add(fuelSupply);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return item;
		}
	}
}