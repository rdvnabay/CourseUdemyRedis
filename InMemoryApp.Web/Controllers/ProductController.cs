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
            //1.Yol
            if (String.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            {
                _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            }

            //2.Yol
            if(!_memoryCache.TryGetValue("zaman",out string zamanCache)){
                _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            }

            //3.Yol
            _memoryCache.GetOrCreate<string>("zaman",entry =>
            {
                return DateTime.Now.ToString();
            });
  
            return View();
        }

        public IActionResult Show()
        {
            ViewBag.Zaman = _memoryCache.Get<string>("zaman");
            return View();
        }
    }
}
