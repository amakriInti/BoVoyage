using BoVoyage.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyage.Scenario1.Controllers
{
    /*-------------------------------------------------
    //Page commerciale
    --------------------------------------------------*/
    public class CommercialController : Controller
    {
        private ClassMetier metier = new ClassMetier();
        /*-------------------------------------------------
        //Tableau des statuts des voyageurs (pas encore fait)
        --------------------------------------------------*/
        public ActionResult Index()
        {
            return View();
        }

        /*----------------------------------
        //Affichage des voyages
        -----------------------------------*/
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

        /*-------------------------------------------------
        //Ajout des voyages CSV
        --------------------------------------------------*/
        public ActionResult AddVoyageCSV()
        {
            try
            {
                metier.AddVoyageCSV_Metier();
                var ps = metier.DBVoyages();
                return RedirectToAction("AffichageVoyage");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

        }

        /*-------------------------------------------------
        //Ajout des voyages formulaire
        --------------------------------------------------*/
        public ActionResult AddVoyageFormulaire()
        {
            return View();
        }
    }
}