
using VKCollectionApi.Core.ViewModels.Category;

namespace VKCollectionApi.Core.Contracts
{
	public interface ICategoryService
	{
		Task<IEnumerable<ProductCategoryViewModel>> GetCategories();

		Task PostCategory(ProductCategoryViewModel category);

		Task<ProductCategoryViewModel> GetCategoryByName(string categoryName);

		Task<ProductCategoryViewModel> GetCategoryById(int id);

		Task<ProductCategoryViewModel> DeleteCategoryById(int id);

		Task<ProductCategoryViewModel> EditCategory(ProductCategoryViewModel model);
	}
}
