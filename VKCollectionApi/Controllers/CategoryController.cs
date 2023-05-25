using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using VKCollectionApi.Core.Contracts;
using VKCollectionApi.Core.ViewModels.Category;

namespace VKCollectionApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController : Controller
	{
		private readonly ICategoryService categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductCategoryViewModel>>> GetCategories()
		{
			var categories = await categoryService.GetCategories();

			return Ok(categories);
		}

		[HttpPost]
		public async Task<IActionResult> PostCategory([FromBody] ProductCategoryViewModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				await categoryService.PostCategory(model);

				var createdCategory = await categoryService.GetCategoryByName(model.Name);

				return Ok(createdCategory);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{id:int}")]
		public async Task<ActionResult<ProductCategoryViewModel>> GetCategoryById(int id)
		{
			try
			{
				var categoryViewModel = await categoryService.GetCategoryById(id);

				return Ok(categoryViewModel);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> DeleteCategory(int id)
		{
			try
			{
				var category = await categoryService.DeleteCategoryById(id);

				return Ok(category);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult> EditCategory(ProductCategoryViewModel model)
		{
			try
			{
				var categoryViewModel = await categoryService.EditCategory(model);

				return Ok(categoryViewModel);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
