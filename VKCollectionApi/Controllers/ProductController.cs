using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using VKCollectionApi.Core.Contracts;
using VKCollectionApi.Core.Services;
using VKCollectionApi.Core.ViewModels.Category;
using VKCollectionApi.Core.ViewModels.Product;

namespace VKCollectionApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : Controller
	{
		private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

		[HttpGet]
        public async Task<IActionResult> GetProducts()
		{
			var products = await productService.GetAllProducts();
			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			try
			{
				var productById = await productService.GetProductById(id);
				return Ok(productById);
			}
			catch (NotFoundException ne)
			{
				return BadRequest(ne);
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddProduct([FromBody] AddProductViewModel model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				await productService.PostProduct(model);

				var createdProduct = await productService.GetProductByName(model.Name);

				return Ok(createdProduct);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteProduct(int id)
		{
			try
			{
				var productViewModel = await productService.DeleteProduct(id);
				return Ok(productViewModel);
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

		[HttpPut("{id}")]
		public async Task<IActionResult> EditCategory(AddProductViewModel model)
		{
			try
			{
				var productViewModel = await productService.EditProduct(model);

				return Ok(productViewModel);
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
