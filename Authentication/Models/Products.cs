using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Models
{
    public class Products
    {
		[Required]
		public int Id { get; set; }

		[Required]
		[StringLength(60)]
		public string Name { get; set; }

		[Required]
		[StringLength(60)]
		public float UnitPrice { get; set; }
	}
}
