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

        /*-------------------------------------------------
        //Ajout des voyages CSV
        --------------------------------------------------*/
        public ActionResult AddVoyageCSV()
        {
            try
            {
                metier.AddVoyageCSV_Metier();
                return RedirectToAction("AffichageVoyage");
            }
            catch (Exception)
            {
                return RedirectToAction("AffichageVoyage");
            }

        }

        /*-------------------------------------------------
        //Ajout des voyages formulaire
        --------------------------------------------------*/
        public ActionResult AddVoyageFormulaire()
        {
            return View();
        }

        /*----------------------------------
        //Panneau de contrôle des dossiers
        -----------------------------------*/
        public ActionResult GestionDossiers()
        {
            List<string> loginCommerciaux = metier.GetLoginCommerciaux();
            return View(loginCommerciaux);
        }
    }
}