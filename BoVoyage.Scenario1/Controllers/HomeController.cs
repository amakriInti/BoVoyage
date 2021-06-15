using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.IO;
using System;
using BoVoyage.Metier;
using System.Collections;

namespace BoVoyage.Scenario1.Controllers
{
    public class HomeController : Controller
    {
        private ClassMetier metier = new ClassMetier();
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult InformationClient(string id)
        //{
        //    var ps = metier.DBVoyages();
        //    try
        //    {
        //        foreach (var item in ps)
        //        {
        //            if (item.Id = id)
        //            {
        //                return View(id);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        public ActionResult InformationClient()
        {
            return View();
        }

        public ActionResult Participant(string nom, string mail, string telephone, string prenom, string personneMorale)
        {
            metier.AddClient(nom, mail, telephone, prenom, personneMorale);
            return View();
        }
        public ActionResult Assurance()
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


    }
}