using System.ComponentModel.DataAnnotations;
using static VKCollectionApi.Infrastructure.Data.DataConstants;

namespace VKCollectionApi.Core.ViewModels.Product
{
	public class AddProductViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "The name is required!")]
		[StringLength(ProductNameMaxLength, MinimumLength = ProductNameMinLength, ErrorMessage = "The name should be at least 4 letters long and maximum of 30 letters.")]
		public string Name { get; set; } = null!;


		[Required(ErrorMessage = "The price is required!")]
		[Range(ProductPriceMinLength, ProductPriceMaxLength, ErrorMessage = "The price should be a number.")]
		public decimal? Price { get; init; }

		[Required(ErrorMessage = "Description is required!")]
		[StringLength(int.MaxValue, MinimumLength = ProductDescriptionMinLength, ErrorMessage = "Description should be a text with minimum of 10 letters.")]
		public string Description { get; init; } = null!;

		[Required]
		public string SellerId { get; set; } = null!;


		public string? ImageUrl { get; set; }

		[Display(Name = "Category")]
		public int CategoryId { get; init; }
	}
}
