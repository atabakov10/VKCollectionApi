using System.ComponentModel.DataAnnotations;

namespace VKCollectionApi.Core.ViewModels.Category
{
	public class ProductCategoryViewModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(ViewModelConstants.CategoryNameMaxLength)]
		public string Name { get; set; } = null!;
	}
}
