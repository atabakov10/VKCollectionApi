namespace VKCollectionApi.Core.ViewModels.Product
{
	public class ProductDetailsViewModel
	{
		public int Id { get; init; }
		public string Name { get; init; }
		public decimal? Price { get; init; }
        public string Description { get; set; }
        public string ImageUrl { get; init; }
		public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SellerId { get; init; }
        public string SellerName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
