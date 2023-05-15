using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using VKCollectionApi.Core.Contracts;
using VKCollectionApi.Core.ViewModels.Product;
using VKCollectionApi.Infrastructure.Data;
using VKCollectionApi.Infrastructure.Data.Models;

namespace VKCollectionApi.Core.Services
{
	public class ProductService : IProductService
	{
		private readonly ApiContext dbContext;

		public ProductService(
			ApiContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<ProductListingViewModel> GetProductByName(string productName)
		{
			var product = await dbContext.Products
				.Include(x => x.ProductCategory)
				.FirstOrDefaultAsync(c => c.Name == productName);

			if (product == null)
			{
				throw new Exception($"Product with name '{productName}' not found.");
			}

			var productViewModel = new ProductListingViewModel
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				ImageUrl = product.ImageUrl,
				Category = product.ProductCategory.Name
			};

			return productViewModel;
		}

		public async Task<ProductListingViewModel> DeleteProduct(int productId)
		{
			var productById = await dbContext.Products.Include(x => x.ProductCategory)
				.FirstOrDefaultAsync(x => x.Id == productId);
			if (productById == null)
			{
				throw new NotFoundException($"Id - {productId} not found!");
			}

			var productViewModel = new ProductListingViewModel
			{
				Id = productById.Id,
				Name = productById.Name,
				Price = productById.Price,
				ImageUrl = productById.ImageUrl,
				Category = productById.ProductCategory.Name
			};

			dbContext.Remove(productById);
			await dbContext.SaveChangesAsync();

			return productViewModel;
		}

		public async Task<IEnumerable<ProductListingViewModel>> GetAllProducts()
		{
			var allProteins = await dbContext.Products
				.Include(x => x.ProductCategory)
				.Select(protein => new ProductListingViewModel()
				{
					Id = protein.Id,
					ImageUrl = protein.ImageUrl,
					Name = protein.Name,
					Price = protein.Price,
					Category = protein.ProductCategory.Name
				})
				.ToListAsync();

			if (allProteins == null)
			{
				throw new NullReferenceException("Something went wrong!");
			}

			return allProteins;
		}



		public async Task PostProduct(AddProductViewModel productViewModel)
		{
			var product = new Product()
			{
				Name = productViewModel.Name,
				Price = (decimal)productViewModel.Price,
				Description = productViewModel.Description,
				ImageUrl = productViewModel.ImageUrl,
				CategoryId = productViewModel.CategoryId,
				SellerId = productViewModel.SellerId
			};

			await dbContext.AddAsync(product);
			await dbContext.SaveChangesAsync();
		}


		public async Task<AddProductViewModel> EditProduct(AddProductViewModel model)
		{
			var product = await dbContext.Products
				.FirstOrDefaultAsync(x => x.Id == model.Id);

			if (product != null)
			{
				product.Description = model.Description;
				product.Name = model.Name;
				product.Price = (decimal)model.Price;
				product.CategoryId = model.CategoryId;
				product.ImageUrl = model.ImageUrl;
				product.SellerId = model.SellerId;

				await dbContext.SaveChangesAsync();
			}
			else
			{
				throw new NotFoundException($"Product with id '{model.Id}' does not exist.");
			}
			return model;
		}


		public async Task<ProductDetailsViewModel?> GetProductById(int id)
		{
			var product = await dbContext.Products
				.Include(x => x.ProductCategory)
				.Include(x => x.Client)
				.FirstOrDefaultAsync(x => x.Id == id);

			if (product == null)
			{
				throw new NotFoundException($"Product with id '{id}' not found.");
			}

			var productViewModel = new ProductDetailsViewModel
			{
				Id = product.Id,
				Name = product.Name,
				Price = product.Price,
				Description = product.Description,
				CategoryId = product.CategoryId,
				CategoryName = product.ProductCategory.Name,
				ImageUrl = product.ImageUrl,
				SellerId = product.SellerId,
				SellerName = $"{product.Client.FirstName} {product.Client.LastName}"
			};

			return productViewModel;
		}
	}
}
