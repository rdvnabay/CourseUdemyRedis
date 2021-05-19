using IDistributedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private IDistributedCache _distributedCache;
        public ProductController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddMinutes(2);

            //Json
            Product product = new Product { Id = 1, Name = "Phone", Price = 4000 };
            string jsonProduct = JsonConvert.SerializeObject(product);
            await _distributedCache.SetStringAsync("product:1", jsonProduct,options);

            //Binary
            Product product2 = new Product { Id = 2, Name = "PC", Price = 6000 };
            string jsonProduct2 = JsonConvert.SerializeObject(product2);
            Byte[] binaryProduct = Encoding.UTF8.GetBytes(jsonProduct2);
            _distributedCache.Set("product:2",binaryProduct,options);
            return View();
        }

        public IActionResult Show() {
            //Json
            string jsonProduct = _distributedCache.GetString("product:1");
            Product product = JsonConvert.DeserializeObject<Product>(jsonProduct);

            //Binary
            Byte[] binaryProduct = _distributedCache.Get("product:2");
            string stringProduct = Encoding.UTF8.GetString(binaryProduct);
            Product product2 = JsonConvert.DeserializeObject<Product>(stringProduct);

            ViewBag.Product = product;
            ViewBag.Product2 = product2;
            return View();
        }

        public IActionResult Remove()
        {
            _distributedCache.Remove("name");
            return View();
        }
    }
}
