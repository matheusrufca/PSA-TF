using Matheus.DAL.Models;

namespace Matheus.Repository.IRepositories
{
	public interface ICarRepository : IRepository<Car>
	{
		Car AddFuelSupply(int carId, FuelSupply item);

		void AddDistanceTravelled(int id, decimal distance);

		void ResetDistanceTravelled(int id);
	}
}