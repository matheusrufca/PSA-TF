using Matheus.DAL.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace Matheus.DAL
{
	public class EFDataContext : DbContext, IUnitOfWork
	{

		public EFDataContext() : base("DefaultConnection")
		{
		}

		public DbSet<Car> Cars { get; set; }


		public DbSet<FuelSupply> FuelSupplies { get; set; }

		public DbSet<FuelType> FuelTypes { get; set; }

		public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
		{
			return base.Set<TEntity>();
		}

		public void Save()
		{
			base.SaveChanges();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Car>();

			//modelBuilder.Configurations.Add(new FuelSupply.FuelSupplyMapping());

			var typesToRegister = Assembly
				.GetExecutingAssembly()
				.GetTypes()
				.Where(type => !String.IsNullOrEmpty(type.Namespace))
				.Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

			foreach (var type in typesToRegister)
			{
				dynamic configurationInstance = Activator.CreateInstance(type);
				modelBuilder.Configurations.Add(configurationInstance);
			}

			base.OnModelCreating(modelBuilder);
		}
	}

	public interface IUnitOfWork
	{
		void Save();
	}
}
