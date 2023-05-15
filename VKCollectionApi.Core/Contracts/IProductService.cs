using VKCollectionApi.Core.ViewModels.Product;
using VKCollectionApi.Infrastructure.Data.Models;

namespace VKCollectionApi.Core.Contracts
{
	public interface IProductService
	{
		Task<ProductListingViewModel> GetProductByName(string productName);

		Task PostProduct(AddProductViewModel productViewModel);

		Task<AddProductViewModel> EditProduct(AddProductViewModel model);

		Task<ProductDetailsViewModel?> GetProductById(int id);

		Task<ProductListingViewModel> DeleteProduct(int productId);

		Task<IEnumerable<ProductListingViewModel>> GetAllProducts();
	}
}
