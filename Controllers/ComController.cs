using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyage.Scenario1.Controllers
{
    [Authorize(Roles ="Commercial, Admin")]
    public class ComController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
    }
}