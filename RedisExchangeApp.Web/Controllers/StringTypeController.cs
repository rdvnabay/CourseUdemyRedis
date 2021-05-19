using Microsoft.AspNetCore.Mvc;
using RedisExchangeApp.Web.Services;
using StackExchange.Redis;
using System;

namespace RedisExchangeApp.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }
        public IActionResult Index()
        {
      
            db.StringSet("name", "Rıdvan Abay");
            db.StringSet("ziyaretci", 100);
            return View();
        }

        public IActionResult Show()
        {
            var value = db.StringGet("name");
            var valueRange = db.StringGetRange("name", 0, 6);
            var valueLength = db.StringLength("name");
            Byte[] imageByte = default(byte[]);
            db.StringSet("image", imageByte);

            db.StringIncrement("ziyaretci", 1);
            var count= db.StringDecrementAsync("ziyaretci", 2).Result;
            db.StringDecrementAsync("ziyaretci", 3).Wait();
            if (value.HasValue)
            {
                ViewBag.Name = value.ToString();
                ViewBag.NameRange = valueRange.ToString();
            }
            return View();
        }
    }
}
