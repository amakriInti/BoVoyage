using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.IO;
using System;
using BoVoyage.Metier;

namespace BoVoyage.Scenario1.Controllers
{
    public class HomeController : Controller
    {
        private ClassMetier metier = new ClassMetier();
        public ActionResult Index()
        {
            return View();
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

       public ActionResult LectureCsv()
        {
            try
            {
                metier.AddVoyage();
                var ps = metier.DBVoyages();
                return View(ps);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult AffichageVoyage()
        {
            try
            {
                var ps = metier.DBVoyages();
                return View(ps);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

        }
    }
}