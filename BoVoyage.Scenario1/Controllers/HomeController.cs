using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.IO;
using System;
using BoVoyage.Metier;
using BoVoyage.Donnees; //(à supprimer après modif vincent)
using System.Collections;
using BoVoyage.Donnees;

namespace BoVoyage.Scenario1.Controllers
{
    public class HomeController : Controller
    {
        private ClassMetier metier = new ClassMetier();
        private Panier panier;
        public HomeController()
        {
            panier = new Panier();
            panier.Voyageurs = new  List<Voyageur>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InformationClient(Guid IdVoyage)
        {
            panier.IdVoyage = IdVoyage;
            Session["panier"] = panier;
            return View();
        }

        /*-------------------------
        //Formulaire participant affichage + envoi formulaire client (httppost)
        --------------------------*/
        public ActionResult Participant(string nom, string mail, string telephone, string prenom, string personneMorale)
        {
            if (Session["panier"] != null) { panier = (Panier)Session["panier"]; }
            panier.Client = metier.CreateClient(nom, mail, telephone, prenom, personneMorale);
            Session["panier"] = panier;
            return View();

        }
        [HttpPost]
        public ActionResult Participant(List<Voyageur> voyageurs)
        {
            panier = (Panier)Session["panier"];
            foreach (var voyageur in voyageurs)
            {

                panier.Voyageurs.Add(metier.CreateVoyageurs(voyageur));
            }
            Session["panier"] = panier;
            return View();
        }

        /*-------------------------
        //Formulaire participant envoi 
        --------------------------*/
        [HttpPost]
        public ActionResult Participant(List<Voyageur> voyageurs)
        {
            panier = (Panier)Session["panier"];
            foreach (var voyageur in voyageurs)
            {

                panier.Voyageurs.Add(metier.CreateVoyageurs(voyageur));
            }
            Session["panier"] = panier;
            return View();
        }

        /*-------------------------
        //Formulaire assurance affichage
        --------------------------*/
        public ActionResult Assurance()
        {
            return View();
        }

        /*-------------------------
        //Formulaire assurance envoie (à mettre en httpPost)
        --------------------------*/
        public ActionResult ValideAssurance(bool assurance)
        {
            panier = (Panier)Session["panier"];
            decimal prix = 100;
            panier.Assurance = metier.CreateAssurance(assurance, prix);
            panier.Dossier = metier.CreateDossier(panier.IdVoyage, panier.Client.Id, panier.Assurance.Id);
            Session["panier"] = panier;
            
            return RedirectToAction("Recap");
        }
        public ActionResult Recap()
        {
            return View();
        }
        public ActionResult Validation()
        {
            panier = (Panier)Session["panier"];
            metier.AddClient(panier.Client);
            metier.AddVoyageurs(panier.Voyageurs);
            metier.AddAssurance(panier.Assurance);
            metier.AddDossier(panier.Dossier);
            metier.AddDossierVoyageurs(panier.Dossier, panier.Voyageurs);
            Session["panier"] = panier;
            return View();
        }

        /*-------------------------
        //A propos
        --------------------------*/
        public ActionResult About()
        {
            return View();
        }

        /*-------------------------
        //Contact
        --------------------------*/
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

        /*----------------------------------
        //Affichage détail des voyages
        -----------------------------------*/
        public ActionResult DetailsVoyage(string id)
        {
            if (id == null) return RedirectToAction("Index");
            var detailvoyage = metier.DetailsVoyage(id);
            if (detailvoyage == null) return RedirectToAction("Index");
            return View(detailvoyage);
        }

    }
    public class Panier
    {
        public Guid IdVoyage { get; set; }
        public Dossier Dossier { get; set; }
        public Assurance Assurance { get; set; }
        public Client Client { get; set; }
        public List<Voyageur> Voyageurs { get; set; }
    }
}