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


			fuelSupply.FuelType = fuelType;
			fuelSupply.FuelPrice = fuelType.Price;
			fuelSupply.TotalPrice = fuelSupply.FuelQuantity * fuelType.Price;
			fuelSupply.FueledAt = DateTime.Now;


			if (fuelSupply.IsNewSerie)
				this.ResetDistanceTravelled(item.Id);

			this.AddDistanceTravelled(item.Id, fuelSupply.DistanceTravelled);

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


		public void AddDistanceTravelled(int id, decimal distance)
		{
			var item = _entitySet.Find(id);

			if (item == null)
				throw new ObjectNotFoundException("Car not found.");

			if (distance < 0)
				throw new ArgumentOutOfRangeException("Distance must be greater then 0");

			item.Odometer.CurrentDistance += distance;
			item.Odometer.TotalDistance += distance;

			base.Edit(item);
		}

		public void ResetDistanceTravelled(int id)
		{
			var item = _entitySet.Find(id);

			if (item == null)
				throw new ObjectNotFoundException("Car not found.");
			
			item.Odometer.CurrentDistance = 0;

			base.Edit(item);
		}
	}
}