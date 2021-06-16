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
            var panier_client = (List<Voyage>)Session["panier"];
            return View(panier_client);
        }

        [Authorize]
        public ActionResult PayerPanier()
        {
            //On crée le dossier 
            Repository Repo = new Repository();
            var Vygs = (List<Voyage>)Session["panier"];
            var Vyg = Vygs.First();
            Repo.NouveauDossier(User.Identity.GetUserName(), Vyg.Id);
            Session["panier"] = new List<Voyage>();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Reserver(Guid? id)//Récupère l'id du voyage choisi
        {
            Repository Repo = new Repository();
            Voyage Vyg = Repo.GetVoyage(id);//permet d'afficher le bon libellé dans le formulaire 
            return View(Vyg);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Reserver(Guid id_v,int nb_voyageurs)//Permet de réserver le voyage avec le bon nombre de passagers
        {
            Repository Repo = new Repository();
            //Repo.NouveauDossier(User.Identity.GetUserName(), vyg.Id);
            Repo.NouveauDossier(User.Identity.GetUserName(), id_v);
            var nombre = nb_voyageurs;
            return RedirectToAction("EntrerVoyageurs", "Home",new { id = id_v, nombre_voyageurs = nb_voyageurs });
        }

        [Authorize]
        public ActionResult EntrerVoyageurs(Guid id, int nombre_voyageurs)//Formulaire
        {
            Repository Repo = new Repository();
            List<Voyageur> Vygrs = new List<Voyageur>();//permet d'afficher le bon libellé dans le formulaire 
            for (int i = 1; i <= nombre_voyageurs; i++)
            {
                Vygrs.Add(new Voyageur());
            }
            ViewBag.nombre_voyageurs = nombre_voyageurs;
            ViewBag.id_voyage =id;
            return View(Vygrs);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EntrerVoyageurs()//Récupère l'id du voyage choisi
        {
            Repository Repo = new Repository();
            int nombre_voyageurs = Convert.ToInt32(System.Web.HttpContext.Current.Request.Form["nb_vg"]);
            for (int i =  0; i < nombre_voyageurs ; i++)
            {
                Voyageur Vygr = new Voyageur {Id = Guid.NewGuid(),
                    Nom = System.Web.HttpContext.Current.Request.Form["name_" + i],
                    Prenom = System.Web.HttpContext.Current.Request.Form["fname_" + i], 
                    DateNaissance = Convert.ToDateTime(System.Web.HttpContext.Current.Request.Form["date_" + i]),
                    IsAccompagnant = Convert.ToBoolean(System.Web.HttpContext.Current.Request.Form["acc_" + i]) };
                Repo.AddVoyageur(Vygr);
                    
            }
            
            return RedirectToAction("Index", "Home");
        }


    }
}