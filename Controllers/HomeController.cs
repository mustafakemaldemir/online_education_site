using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using online_education_site.EntityFramework.Models;
using online_education_site.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace online_education_site.Controllers
{
    public class HomeController : Controller
    {       

        private readonly online_educationContext _veritabani;

        public HomeController(online_educationContext context)
        {
            _veritabani = context;
        }        

        public IActionResult Index()
        {
            return View();
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
