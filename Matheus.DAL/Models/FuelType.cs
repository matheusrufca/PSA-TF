using System.ComponentModel.DataAnnotations.Schema;

namespace Matheus.DAL.Models
{
	public class FuelType
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }
	}
}