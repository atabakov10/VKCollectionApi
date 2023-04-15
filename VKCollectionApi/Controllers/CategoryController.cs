using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VKCollectionApi.Core.ViewModels.Category;
using VKCollectionApi.Infrastructure.Data;
using VKCollectionApi.Infrastructure.Data.Models;

namespace VKCollectionApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController : Controller
	{
		private readonly ApiContext dbContext;

		public CategoryController(ApiContext context)
		{
			dbContext = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductCategoryViewModel>>> GetCategories()
		{
			var categories = await dbContext.ProductCategories.ToListAsync();

			return Ok(categories);
		}

		[HttpPost]
		public async Task<IActionResult> PostCategory(ProductCategoryViewModel model)
		{
			if (model == null)
			{
				return BadRequest(ModelState);
			}

			var category = new ProductCategories
			{
				Name = model.Name
			};

			await dbContext.AddAsync(category);
			await dbContext.SaveChangesAsync();
			return Ok(category);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetCategory(int id)
		{
			var category = await dbContext.ProductCategories.FindAsync(id);

			if (category == null)
			{
				return NotFound();
			}

			return Ok(category);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteCategory(int id)
		{
			var category = await dbContext.ProductCategories.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}
			dbContext.Remove(category);
			await dbContext.SaveChangesAsync();
			return Ok(category);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> PutCategory(ProductCategoryViewModel model)
		{
			var product = await dbContext.ProductCategories.FirstOrDefaultAsync(x=> x.Id == model.Id);

			if (product != null)
			{
				product.Name = model.Name;

				await dbContext.SaveChangesAsync();
			}
			else
			{
				return NotFound();
			}

			return Ok(product);
		}
	}
}
