using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DataStorageProject.Controllers
{
    public class TestController : Controller
    {
        //通过依赖注入使用cache
        private readonly IMemoryCache _cache;
        public TestController(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IActionResult Index()
        {
            ViewData["key"] = 1;                    //不可跨试图
            TempData["username"] = "张三";        //可跨试图


            //Session属于会话级别的
            ISession session = HttpContext.Session;
            session.SetInt32("age", 15);
            session.SetString("name", "gamecc666");

            //通过依赖注入获取cache对象，属于应用级别的
            _cache.Set("age", 30);
            _cache.Set("realname", "gamecc");     

            return View();
        }
        public IActionResult Read()
        {
            //ISession session = HttpContext.Session;
            //int? age= session.GetInt32("age");
            //if(age.HasValue)
            //{
            //    //可以通w过此属性进行判断处理不同情况
            //    ViewBag.age = age;
            //}
            //string name = session.GetString("name");
            //ViewBag.realname = name;

            //获取cache数据
            int age = _cache.Get<int>("age");
            string realname = _cache.Get<string>("realname");

            ViewBag.realname = realname;
            ViewBag.age = age;

            return View();
        }
    }
}