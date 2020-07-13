using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MyCompany.Areas.Admin.Controllers
{
    public class ServiceItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}