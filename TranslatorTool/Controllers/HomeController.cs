using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TranslatorTool.Helpers;
using TranslatorTool.Models;

namespace TranslatorTool.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            Translation ts = TranslationHelper.GetTranslation(id);
            if(ts != null)
                return View("~/Views/Home/View.cshtml", ts);

            return Redirect("/");
        }

        // GET api/values/5
        [HttpPost]
        public JsonResult Create([FromForm] string from, [FromForm] string to)
        {
            int existingId = 0;
            if(!TranslationHelper.Exists(from, out existingId))
            {
                existingId = TranslationHelper.AddTranslation(from, to);
            }

            Translation ts = TranslationHelper.GetTranslation(existingId);

            return new JsonResult(ts, new Newtonsoft.Json.JsonSerializerSettings()
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented
                });

        }

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
