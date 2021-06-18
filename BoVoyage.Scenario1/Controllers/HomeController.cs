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
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        /*----------------------------------
        //Ajout des voyages depuis fichier CSV
        -----------------------------------*/
        public ActionResult LectureCsv()
        {
            try
            {
                metier.AddVoyage();
                //var ps = metier.DBVoyages(choix);
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

        }

        /*----------------------------------
        //Affichage des voyages
        -----------------------------------*/
        //public ActionResult AffichageVoyage(string lieu, string txtlieu)
        //{
        //    try
        //    {
        //        var ps = metier.DBVoyages(lieu, txtlieu);
        //        return View(ps);
        //    }
        //    catch (Exception)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //}
        public ActionResult DetailsVoyage(string id)
        {
            if (id == null) return RedirectToAction("Index");
            var detailvoyage = metier.DetailsVoyage(id);
            if (detailvoyage == null) return RedirectToAction("Index");
            return View(detailvoyage);
        }

        [HttpPost]
        public ActionResult MotDePasse(int? code)
        {
            if (code == 1234)
            {
                Session["Passe"] = true;
                return RedirectToAction("Acceuil", "Photo");
            }
            if (code != null) ViewBag.Erreur = "Code incorrect";
            return View();
        }
    }
}