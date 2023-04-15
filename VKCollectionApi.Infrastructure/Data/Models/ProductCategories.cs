namespace VKCollectionApi.Infrastructure.Data.Models
{
	public class ProductCategories
	{
		public int Id { get; init; }

		public string Name { get; set; } = null!;

		public ICollection<Product> Products { get; init; } = new HashSet<Product>();
	}
}
