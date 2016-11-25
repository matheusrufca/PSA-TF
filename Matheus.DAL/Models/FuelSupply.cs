using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Matheus.DAL.Models
{
	public class FuelSupply
	{

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public virtual Car CarSupplied { get; set; }

		public virtual FuelType FuelType { get; set; }


		public int FuelQuantity { get; set; }

		public decimal TotalPrice { get; set; }

		public decimal FuelPrice { get; set; }

		public DateTime FueledAt { get; set; }

		public bool IsNewSerie { get; set; }

		public class FuelSupplyMapping : EntityTypeConfiguration<FuelSupply>
		{
			public FuelSupplyMapping()
			{
				HasRequired(x => x.FuelType);
				HasRequired(x => x.CarSupplied);
			}
		}
	}
}