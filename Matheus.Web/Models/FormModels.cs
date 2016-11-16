using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
	public class CarFormViewModel {
		public string LicencePlate { get; set; }

		public string Model { get; set; }

		public int Year { get; set; }

		public string Manufacturer { get; set; }

		public decimal FuelTankSize { get; set; }

		public OdometerModel Odometer { get; set; }
	}


	public class CreateCarViewModel : CarFormViewModel
	{
		
	}


	public class EditCarViewModel : CarFormViewModel
	{
	}


	public class AddFuelSupplyViewModel
	{
		public string FuelType { get; set; }

		public string FuelQuantity { get; set; }

		public string TotalPrice { get; set; }

		public string FuelPrice { get; set; }
	}

}