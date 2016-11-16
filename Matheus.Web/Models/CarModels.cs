using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
	public class CarModel
	{
		public string LicencePlate { get; set; }

		public string Model { get; set; }

		public int Year { get; set; }

		public string Manufacturer { get; set; }

		public decimal FuelTankSize { get; set; }

		public OdometerModel Odometer { get; set; }

	}


	public class OdometerModel
	{
		public int CurrentDistance { get; set; }
		public int TotalDistance { get; set; }
	}

	public class FuelSupplyModel
	{
		public string FuelQuantity { get; set; }

		public string TotalPrice { get; set; }

		public FuelTypeModel FuelType { get; set; }
	}


	public class FuelTypeModel {
		public string Name { get; set; }
		public string Price { get; set; }
	}
}