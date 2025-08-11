using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using url_shortener.MVC.Models;

namespace url_shortener.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Store shortened URLs in memory for now
        private static Dictionary<string, string> _shortUrls = new Dictionary<string, string>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST: Shorten a URL
        [HttpPost]
        public IActionResult ShortenUrl(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                ViewData["Error"] = "Please enter a valid URL.";
                return View("Index");
            }

            // Generate random short code
            var shortCode = GenerateShortCode(8);

            // Store mapping
            _shortUrls[shortCode] = originalUrl;

            // Create full shortened link
            var shortUrl = $"{Request.Scheme}://{Request.Host}/{shortCode}";

            // Send to a ShortUrl view page
            ViewData["ShortUrl"] = shortUrl;
            return View("ShortUrl");
        }

        // Redirect when accessing the short link
        [Route("{shortCode}")]
        public IActionResult RedirectToOriginal(string shortCode)
        {
            if (_shortUrls.TryGetValue(shortCode, out var originalUrl))
            {
                return Redirect(originalUrl);
            }

            return NotFound("Short URL not found.");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Random code generator
        private static string GenerateShortCode(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
