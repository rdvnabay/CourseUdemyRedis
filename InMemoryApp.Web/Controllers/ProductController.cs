using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private IMemoryCache _memoryCache;
        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
            options.Priority = CacheItemPriority.High;
            //RegisterPostEvictionCallback
            options.RegisterPostEvictionCallback((key, value, reason,state) =>
            {
                _memoryCache.Set("callback", $"{key} - {value}  {reason}");
            });
            _memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);
            return View();
        }

        public IActionResult Show()
        {
            _memoryCache.TryGetValue("zaman", out string zamanCache);
            _memoryCache.TryGetValue("callback", out string callback);
            ViewBag.Zaman = zamanCache;
            ViewBag.Callback = callback;
            return View();
        }
    }
}
