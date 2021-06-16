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
        public ActionResult Assurance(string nom, string prenom, DateTime naissance, bool isAccompagnant, string mail)
        {
            metier.AddVoyageurs(nom, prenom, naissance, isAccompagnant, mail);
            return RedirectToAction("Participant");
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
        //Affichage des voyages
        -----------------------------------*/
        public ActionResult AffichageVoyage(string lieu, string txtlieu)
        {
            try
            {
                var ps = metier.DBVoyages(lieu, txtlieu);
                return View(ps);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

    }
}