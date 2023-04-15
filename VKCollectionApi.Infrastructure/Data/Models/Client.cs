using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VKCollectionApi.Infrastructure.Data.Models
{
	public class Client : IdentityUser
	{
		[Required]
		public string? FirstName { get; set; }

		[Required]
		public string? LastName { get; set; }

		public ICollection<Product> ProductsCreated { get; set; } = new HashSet<Product>();
	}
}
