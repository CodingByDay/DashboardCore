using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDashboardAspNetCore3.Controllers
{
    public class APIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            var admin = Guid.Empty;
            return Get(admin);
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            HttpContext.Session.SetString("user", id.ToString());
            return View();
        }
    }
}
