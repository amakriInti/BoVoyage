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
            if (Session["panier"] != null)// Si panier est nul, on retourne  à l'accueil
            {
                var panier_client = (List<ItemPanier>)Session["panier"];
                return View(panier_client);
            }
            else return RedirectToAction("Index", "Home");
        }

        [Authorize]

        public ActionResult Confirmation()// On va simplement faire afficher dans 
        {
            if (Session["panier"] != null)// Si panier est nul, on retourne  à l'accueil
            {
                List<ItemPanier> panier_courant = (List<ItemPanier>)Session["panier"];
                ViewBag.NomClient = User.Identity.GetUserName();// On mettra le login pour l'instant
                return View(panier_courant);
            }
            else return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Payer()
        {

            if (Session["panier"] != null)
            {
                Repository Repo = new Repository();
                List<ItemPanier> panier_courant = (List<ItemPanier>)Session["panier"];
                foreach (var item in panier_courant)
                {
                    Repo.DossierPaye(item.id_dossier);
                }
                return RedirectToAction("Index", "Home");
            }
            else return RedirectToAction("Index", "Home");

        }
    }
}