using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLove.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{errorCode}")]
        public IActionResult ErrorCodeHandler(int errorCode)
        {
            switch (errorCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "The page you requested does not exit";
                    break;
            }
            return View("AccessError");
        }
    }
}
