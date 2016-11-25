using Matheus.DAL;
using Matheus.DAL.Models;
using Matheus.Repository.IRepositories;

namespace Matheus.Repository
{
	public class FuelTypeRepository : BaseRepository<FuelType>, IFuelTypeRepository
	{
		public FuelTypeRepository(IUnitOfWork context) : base(context)
		{
		}
	}
}