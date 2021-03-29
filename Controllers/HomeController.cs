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

        public IActionResult LOGIN()
        {
            return View();
        }

        public IActionResult REGISTER()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Student_LOGIN()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Student_REGISTER()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Teacher_LOGIN()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Teacher_REGISTER()
        {
            return View();
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
