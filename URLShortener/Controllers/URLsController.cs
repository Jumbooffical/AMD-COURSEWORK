using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using URLShortener.Data;
using URLShortener.Models;

namespace URLShortener.Controllers
{
	public class URLsController : Controller
	{
		private readonly URLDbContext _context;

		public URLsController(URLDbContext context)
		{
			_context = context;
		}

		// GET: URLs
		public async Task<IActionResult> Index()
		{
			var host = $"{Request.Scheme}://{Request.Host}";
			ViewBag.Host = host;
			return View(await _context.URLs.ToListAsync());
		}

		// DELETE: URLs
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Guid id)
		{
			var item = _context.URLs.Find(id);
			if (item != null)
			{
				_context.URLs.Remove(item);
				_context.SaveChanges();
			}
			return await Index();
		}

		[HttpGet]
		public IActionResult Shorten()
		{
			return View();
		}

		// Shortens the URL.
		[HttpPost]
		public async Task<IActionResult> Shorten(string originalUrl)
		{
			// Get first 8 chars of GUID as link
			var shortCode = Guid.NewGuid().ToString().Substring(0, 8);
			var shortUrl = new URL { OriginalUrl = originalUrl, ShortCode = shortCode };

			// Add to database & save changes
			_context.URLs.Add(shortUrl);
			_context.SaveChanges();

			// Craft a link from the short code: https://host/shortCode.
			ViewBag.URL = $"{Request.Scheme}://{Request.Host}/{shortCode}";

			// Go back to Index (must wait until data is updated).
			return await Index();
		}

		// Redirect to original link when shortened URL is typed.
		[HttpGet("/{code}")]
		public IActionResult RedirectToOriginal(string code)
		{
			// Find destination URL from shortened URL
			var url = _context.URLs.FirstOrDefault(u => u.ShortCode == code);
			if (url == null)
				return NotFound();

			return Redirect(url.OriginalUrl);
		}
	}
}
