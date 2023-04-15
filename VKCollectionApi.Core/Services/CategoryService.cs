using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using VKCollectionApi.Core.Contracts;
using VKCollectionApi.Core.ViewModels.Category;
using VKCollectionApi.Infrastructure.Data;
using VKCollectionApi.Infrastructure.Data.Models;

namespace VKCollectionApi.Core.Services
{

	public class CategoryService : ICategoryService
	{
		private readonly ApiContext dbContext;

		public CategoryService(ApiContext context)
        {
            this.dbContext = context;
        }
		public async Task<IEnumerable<ProductCategoryViewModel>> GetCategories()
		{
			var categories = await dbContext.ProductCategories.ToListAsync();

			var categoryViewModels = categories.Select(category => new ProductCategoryViewModel
			{
				Id = category.Id,
				Name = category.Name
			});

			return categoryViewModels;
		}

		public async Task PostCategory(ProductCategoryViewModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model), "Invalid request");
			}

			var category = new ProductCategories
			{
				Name = model.Name
			};

			await dbContext.AddAsync(category);
			await dbContext.SaveChangesAsync();
		}

		public async Task<ProductCategoryViewModel> GetCategoryByName(string categoryName)
		{
			var category = await dbContext.ProductCategories
				.FirstOrDefaultAsync(c => c.Name == categoryName);

			if (category == null)
			{
				throw new Exception($"Category with name '{categoryName}' not found.");
			}

			var categoryViewModel = new ProductCategoryViewModel
			{
				Id = category.Id,
				Name = category.Name
			};

			return categoryViewModel;
		}

		public async Task<ProductCategoryViewModel> GetCategoryById(int id)
		{
			var category = await dbContext.ProductCategories.FindAsync(id);

			if (category == null)
			{
				throw new NotFoundException($"Category with id '{id}' not found.");
			}

			var categoryViewModel = new ProductCategoryViewModel
			{
				Id = category.Id,
				Name = category.Name
			};

			return categoryViewModel;
		}

		public async Task<ProductCategoryViewModel> DeleteCategoryById(int id)
		{
			var category = await dbContext.ProductCategories.FindAsync(id);
			if (category == null)
			{
				throw new NotFoundException($"Category with id '{id}' not found.");
			}

			var categoryViewModel = new ProductCategoryViewModel
			{
				Id = category.Id,
				Name = category.Name
			};

			dbContext.Remove(category);
			await dbContext.SaveChangesAsync();

			return categoryViewModel;
		}

		public async Task<ProductCategoryViewModel> EditCategory(ProductCategoryViewModel model)
		{
			var category = await dbContext.ProductCategories.FirstOrDefaultAsync(x => x.Id == model.Id);

			if (category != null)
			{
				category.Name = model.Name;

				await dbContext.SaveChangesAsync();
			}
			else
			{
				throw new NotFoundException($"Category with id '{model.Id}' does not exist.");
			}
			return model;
		}
	}
}
