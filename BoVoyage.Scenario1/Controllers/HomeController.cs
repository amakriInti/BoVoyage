using BoVoyage.Scenario1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BoVoyage.Scenario1.Controllers
{
    public class HomeController : Controller
    {
        TraitementBD tBD = new TraitementBD();
        public ActionResult Index()
        {
            List<Voyage> vD = tBD.GetVoyages();
            
            return View(vD);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult VoyageDescription(Guid id)
        {
            List<Voyage> vD = tBD.GetVoyages();

            var voyageSelectionne = vD
                .Where(v=>v.Id==id)
                .FirstOrDefault();


            return View(voyageSelectionne);
        }
    }
}