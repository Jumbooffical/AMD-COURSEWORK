using Microsoft.AspNetCore.Mvc;
using URLShortener.Data;

namespace URLShortener.Controllers
{
	public class URLsController : Controller
	{
		private readonly URLDbContext _context;

		public URLsController(URLDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			//var urls = _context.URLs.ToList();
			return View();
		}
	}
}
