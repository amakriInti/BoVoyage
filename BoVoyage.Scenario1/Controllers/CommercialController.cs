using BoVoyage.Metier;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        [HttpPost]
        public ActionResult AddVoyageFormulaire(string id)
        {
            string fournisseur = Request.Form["Fournisseur"];
            if (fournisseur == "Autre")
            {
                fournisseur = Request.Form["NewFournisseur"];
            }
            string dateDepart = Request.Form["DateDepart"];
            string dateRetour = Request.Form["DateRetour"];
            string nbPlace = Request.Form["NbPlace"];
            string prixAchat = Request.Form["PrixAchat"].Replace('.', ',');
            string prixVente = Request.Form["PrixVente"].Replace('.', ',');
            string descriptionVoyage = Request.Form["DescriptionVoyage"];
            string continent = Request.Form["Continent"];
            string pays = Request.Form["Pays"];
            if (pays == "Autre")
            {
                pays = Request.Form["NewPays"];
            }
            string region = Request.Form["Region"];
            if (region == "Autre")
            {
                region = Request.Form["NewRegion"];
            }
            string DescriptionDestination = Request.Form["DescriptionDestination"];
            string[] NewVoyage = { dateDepart, dateRetour, nbPlace, fournisseur, prixAchat, prixVente, descriptionVoyage, continent, pays, region, DescriptionDestination };

            var RetourAjout = metier.AddVoyageFormulaire_Metier(NewVoyage);

            return RedirectToAction("AffichageVoyage");
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