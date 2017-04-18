using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainApplication.Controllers
{
    [Route("views/[controller]")]
    public class HomeController : Controller
    {
        [Route("[action]")]
        [Route("/", Order = -1)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
