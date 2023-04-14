using Microsoft.AspNetCore.Mvc;

namespace VKCollectionApi.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
