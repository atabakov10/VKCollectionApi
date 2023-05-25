namespace VKCollectionApi.Core.ViewModels.Product
{
	public class ProductListingViewModel
	{
		public int Id { get; init; }
		public string Name { get; init; }
		public decimal? Price { get; init; }
		public string ImageUrl { get; init; }
		public string Category { get; init; }
		public DateTime CreatedOn { get; set; }
	}
}
