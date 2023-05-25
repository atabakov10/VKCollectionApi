using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static VKCollectionApi.Infrastructure.Data.DataConstants;

namespace VKCollectionApi.Infrastructure.Data.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(ProductNameMaxLength)]
		public string Name { get; set; } = null!;

		[Required]
		[MaxLength(ProductPriceMaxLength)]
		[Column(TypeName = "decimal(6,2)")]
		public decimal Price { get; set; }

		[Required]
		public string Description { get; set; } = null!;

		public DateTime CreatedOn { get; set; }

		[Required]
		[Url]
		public string ImageUrl { get; set; } = null!;

		public int CategoryId { get; set; }
		[ForeignKey(nameof(CategoryId))]
		public ProductCategories ProductCategory { get; init; } = null!;

		[Required]
		public string SellerId { get; set; } = null!;

		[ForeignKey(nameof(SellerId))]
		public Client Client { get; set; } = null!;
	}
}
