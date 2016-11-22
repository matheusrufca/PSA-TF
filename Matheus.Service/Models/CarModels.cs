using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Web.Models
{
	public class CarModel
	{
		public Guid Id { get; set; }

		public string LicencePlate { get; set; }

		public string Model { get; set; }

		public int Year { get; set; }

		public string Manufacturer { get; set; }

		public decimal FuelTankSize { get; set; }

		public OdometerModel Odometer { get; set; }


		public IEnumerable<FuelSupplyModel> FuelSupplies { get; set; }
	}


	public class OdometerModel
	{
		public int CurrentDistance { get; set; }
		public int TotalDistance { get; set; }
	}

	public class FuelSupplyModel
	{
		public Guid Id { get; set; }

		public FuelTypeModel FuelType { get; set; }

		public string FuelQuantity { get; set; }

		public string TotalPrice { get; set; }

		public string FuelPrice { get; set; }

		public DateTime FueledAt { get; set; }

		public bool IsNewSerie { get; set; }
	}


	public class FuelTypeModel
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }
	}
}