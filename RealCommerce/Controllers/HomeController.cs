using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RealCommerce.Models;
using RealCommerce.Services;
using System.Diagnostics;

namespace RealCommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly newsService _newsService;

        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache, newsService newsService)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _newsService = newsService;
        }

        public ActionResult Index()
        {
            string cacheKey = "NewsFeed";
            List<Post> newsItems;

            // Try to retrieve the data from the cache
            if (!_memoryCache.TryGetValue(cacheKey, out newsItems))
            {
                // Fetch news items using NewsService
                newsItems = _newsService.GetNewsItemsFromRSS();

                // Store data in cache
                _memoryCache.Set(cacheKey, newsItems, TimeSpan.FromHours(1));
            }

            return View(newsItems);
        }

        [HttpGet]
        public async Task<IActionResult> GetPostBody(string url)
        {
            try
            {
                Console.WriteLine("in home controller:)");
                // Fetch the body of the post asynchronously using its URL
                string body = await _newsService.GetPostBodyAsync(url);
                return Ok(body);
            }
            catch (Exception ex)
            {
                return BadRequest("Error fetching post body: " + ex.Message);
            }
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
    }
}