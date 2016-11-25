using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matheus.Data
{
	public abstract class BaseEntity
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
	}


	public class Car
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CarId { get; set; }
		
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
		public int FuelSupplyId { get; set; }

		public FuelType FuelType { get; set; }

		public string FuelQuantity { get; set; }

		public string TotalPrice { get; set; }

		public string FuelPrice { get; set; }

		public DateTime FueledAt { get; set; }

		public bool IsNewSerie { get; set; }


		public virtual Car CarSupplied { get; set; }
	}


	public class FuelType
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }
	}
}
