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

        // Constructor for HomeController
        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache, newsService newsService)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _newsService = newsService;
        }

        // Action method for the Index page
        public ActionResult Index()
        {
            string cacheKey = "NewsFeed";
            List<Post> newsItems;

            // Try to retrieve the data from the cache
            if (!_memoryCache.TryGetValue(cacheKey, out newsItems))
            {
                // Fetch news items using NewsService
                newsItems = _newsService.GetNewsItemsFromRSS();

                // Store data in cache for 1 hour
                _memoryCache.Set(cacheKey, newsItems, TimeSpan.FromHours(1));
            }

            return View(newsItems);
        }

        // Action method to get post body by id
        [HttpGet]
        public IActionResult GetPostBody(string id)
        {
            try
            {
                string body = _newsService.GetPostBodyById(id);
                return Ok(body); // Return body if successful
            }
            catch (Exception ex)
            {
                return BadRequest("Error fetching post body: " + ex.Message); // Return error message if there's an exception
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
