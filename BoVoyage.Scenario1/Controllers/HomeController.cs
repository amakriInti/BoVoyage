﻿using BoVoyage.Scenario1.Dal;
using BoVoyage.Scenario1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace BoVoyage.Scenario1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Repository Repo = new Repository();
            List<Voyage> Voyages_Accueil = Repo.GetAllVoyages();
            return View(Voyages_Accueil);
        }


        public ActionResult UpdatePanier()
        {
            if(Session["panier"]==null)//Si le panier existe pas on le crée sinon on le récupère
            { Session["panier"] = new List<Voyage>(); }
            List<Voyage> panier_courant = (List<Voyage>)Session["panier"];
            Repository Repo = new Repository();

            panier_courant.Add(Repo.GetVoyage(Guid.Parse("dd1e74fb-f2e4-4cbd-978d-11a9f484b781")));
            Session["panier"] = panier_courant;
            //Session["panier"] = Repo.GetVoyage(Guid.Parse("dd1e74fb-f2e4-4cbd-978d-11a9f484b781"));//On a mis l'id en dur pour le moment
            return View("Index");
        }
        //Session["personne"] = id; Ligne qui crée le panier 

        public ActionResult Panier()
        {
            var panier = Session["panier"];
            return View(panier);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult ValiderPanier()
        {
            var panier_client = (List<ItemPanier>)Session["panier"];
            return View(panier_client);
        }

        [Authorize]

        public ActionResult Confirmation()// On va simplement faire afficher dans 
        {
            /*Repository Repo = new Repository();
            Dossier Doss = Repo.GetDossier(id_dossier);//je pourrais créer le dossier ici
            return View(Doss);*/
            List<ItemPanier> panier_courant = (List<ItemPanier>)Session["panier"];
            ViewBag.NomClient = User.Identity.GetUserName();// On mettra le login pour l'instant
            return View(panier_courant);
        }

        public ActionResult Payer()
        {
            Repository Repo = new Repository();
            List<ItemPanier>panier_courant = (List<ItemPanier>)Session["panier"];
            foreach (var item in panier_courant)
            {
                Repo.DossierPaye(item.id_dossier);
            }
            return RedirectToAction("Index", "Home");
        }

        /*public ActionResult ValiderPanier()
        {
            var panier_client = (List<Voyage>)Session["panier"];
            return View(panier_client);
        }*/


    }
}