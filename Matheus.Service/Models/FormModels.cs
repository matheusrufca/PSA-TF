using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
	public class CarFormViewModel
	{
		[Required]
		public string LicencePlate { get; set; }

		[Required]
		public string Model { get; set; }

		[Required]
		public int? Year { get; set; }

		[Required]
		[StringLength(7)]
		public string Manufacturer { get; set; }

		[Required]
		public decimal FuelTankSize { get; set; }
		
		
	}


	public class CreateCarViewModel : CarFormViewModel
	{

	}


	public class EditCarViewModel : CarFormViewModel
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public OdometerModel Odometer { get; set; }
	}


	public class AddFuelSupplyViewModel
	{
		public string FuelType { get; set; }

		public string FuelQuantity { get; set; }

		public string TotalPrice { get; set; }

		public string FuelPrice { get; set; }


		public DateTime FueledAt { get; set; }

		public bool IsNewSerie { get; set; }
	}

}