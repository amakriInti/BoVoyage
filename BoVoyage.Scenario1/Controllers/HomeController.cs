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

        public ActionResult InformationClient(Guid IdVoyage)
        {
            Session["Voyage"] = IdVoyage;
            return View();
        }

        public ActionResult Participant(string nom, string mail, string telephone, string prenom, string personneMorale)
        {
            Guid IdClient = metier.AddClient(nom, mail, telephone, prenom, personneMorale);
            Session["Client"] = IdClient;
            return View();
        }
        public ActionResult Assurance()
        {
            return View();
        }
        public ActionResult ValideAssurance(bool assurance)
        {
            decimal prix = 100;
            Guid IdAssurance = metier.CreateAssurance(assurance, prix);
            var IdVoyage = (Guid)Session["Voyage"];
            var IdClient = (Guid)Session["Client"];
            Guid IdDossier = metier.CreateDossier(IdVoyage, IdClient, IdAssurance);
            return RedirectToAction("Index");
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
        public ActionResult AffichageVoyage(string tri, string choix)
        {
            try
            {
                var ps = metier.DBVoyages(tri, choix);
                return View(ps);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult DetailsVoyage(string id)
        {
            if (id == null) return RedirectToAction("Index");
            var detailvoyage = metier.DetailsVoyage(id);
            if (detailvoyage == null) return RedirectToAction("Index");
            return View(detailvoyage);
        }
    }
}