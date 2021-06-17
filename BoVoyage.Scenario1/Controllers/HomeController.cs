using BoVoyage.Scenario1.Dal;
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
        public ActionResult Reserver(Guid id)//Récupère l'id du voyage choisi
        {
            Repository Repo = new Repository();
            Voyage Vyg = Repo.GetVoyage(id);//permet d'afficher le bon libellé dans le formulaire 
            return View(Vyg);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Reserver(int nb_voyageurs)//Permet de réserver le voyage avec le bon nombre de passagers
        {
            Repository Repo = new Repository();
            Guid id_voyage = Guid.Parse(System.Web.HttpContext.Current.Request.Form["id_voyage"]);//On récupère l'id du voyage
            //Repo.NouveauDossier(User.Identity.GetUserName(), vyg.Id);
            //Guid? id_dossier = Repo.NouveauDossier(User.Identity.GetUserName(), id_voyage);
            if (Session["panier"] == null)//Si le panier existe pas on le crée sinon on le récupère
            { Session["panier"] = new List<ItemPanier>(); }
            List<ItemPanier> panier_courant = (List<ItemPanier>)Session["panier"];//récupère le panier
            panier_courant.Add(new ItemPanier { voyage = Repo.GetVoyage(id_voyage), nombre_voyageurs = nb_voyageurs });
            Session["panier"] = panier_courant;
            return RedirectToAction("EntrerVoyageurs", "Home",new { nombre_voyageurs = nb_voyageurs });
        }

        [Authorize]
        public ActionResult EntrerVoyageurs(int nombre_voyageurs)//Formulaire
        {
            Repository Repo = new Repository();
            List<Voyageur> Vygrs = new List<Voyageur>();//permet d'afficher le bon libellé dans le formulaire 
            for (int i = 1; i <= nombre_voyageurs; i++)
            {
                Vygrs.Add(new Voyageur());
            }
            ViewBag.nombre_voyageurs = nombre_voyageurs;
            //ViewBag.id_dossier =id_doss;
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
                Guid Id_nouveau_voyageur = Guid.NewGuid();
                Voyageur Vygr = new Voyageur { Id = Id_nouveau_voyageur,
                    Nom = System.Web.HttpContext.Current.Request.Form["name_" + i],
                    Prenom = System.Web.HttpContext.Current.Request.Form["fname_" + i],
                    DateNaissance = Convert.ToDateTime(System.Web.HttpContext.Current.Request.Form["date_" + i]),
                    IsAccompagnant = Convert.ToBoolean(System.Web.HttpContext.Current.Request.Form["acc_" + i]),
                };

                Repo.AddVoyageur(Vygr);
                
                //Une fois les voyageurs ajoutés On met le voyage au panier



            }
            
            return RedirectToAction("Confirmation", "Home");// va à confirmation... on ne crée pas de dossier pour l'instant
        }

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
            //Repo.DossierPaye(id_dossier);
            return RedirectToAction("Index", "Home");
        }


    }
}