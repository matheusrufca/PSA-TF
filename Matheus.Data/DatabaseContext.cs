using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matheus.Data
{
	public class BlogContext : DbContext
	{
		public DbSet<Blog> Blogs { get; set; }
	}

	

	public class DataContext : DbContext
	{

		public DataContext() : base("DefaultConnection")
		{
		}

		public DbSet<Car> Cars { get; set; }


		public DbSet<FuelSupply> FuelSupplies { get; set; }

		public DbSet<FuelType> FuelTypes{ get; set; }
	}
}
