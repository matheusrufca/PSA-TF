using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matheus.Data
{
	
	public class Blog
	{
		public int BlogId { get; set; }

		public string Name { get; set; }
	}


	

	public class Car
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid CarId { get; set; }
		
		public string LicencePlate { get; set; }

		public string Model { get; set; }

		public int Year { get; set; }

		public string Manufacturer { get; set; }

		public decimal FuelTankSize { get; set; }

		public Odometer Odometer { get; set; }


		public virtual ICollection<FuelSupply> FuelSupplies { get; set; }
	}

	public class Odometer
	{
		public int CurrentDistance { get; set; }

		public int TotalDistance { get; set; }
	}

	public class FuelSupply
	{

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid FuelSupplyId { get; set; }

		public string FuelType { get; set; }

		public string FuelQuantity { get; set; }

		public string TotalPrice { get; set; }

		public string FuelPrice { get; set; }

		public bool isNewSerie { get; set; }


		public virtual Car CarSupplied { get; set; }
	}


	public class FuelType
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }
	}
}
