using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Matheus.DAL.Models
{
	public class Car
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		
		public string LicencePlate { get; set; }

		public string Model { get; set; }

		public int Year { get; set; }

		public string Manufacturer { get; set; }

		public decimal FuelTankSize { get; set; }

		public Odometer Odometer { get; set; }


		public virtual ICollection<FuelSupply> FuelSupplies { get; set; }

		public class CarMapping : EntityTypeConfiguration<Car>
		{
			public CarMapping()
			{
			}
		}
	}

	public class Odometer
	{
		public int CurrentDistance { get; set; }

		public int TotalDistance { get; set; }
	}
}