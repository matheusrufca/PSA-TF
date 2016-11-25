using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Matheus.DAL.Models
{
	public abstract class BaseEntity
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
	}
}