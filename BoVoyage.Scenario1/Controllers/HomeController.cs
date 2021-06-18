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
        public static Repository Repo = new Repository();
        public ActionResult Index()
        {
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
                List<ItemPanier> panier_courant = (List<ItemPanier>)Session["panier"];
                foreach (var item in panier_courant)
                {
                    Repo.DossierPaye(item.id_dossier);
                }
                return RedirectToAction("Index", "Home");
            }
            else return RedirectToAction("Index", "Home");

        }

        public ActionResult PayerCarte()
        {

            if (Session["panier"] != null)// Si panier est nul, on retourne  à l'accueil
            {
                List<ItemPanier> panier_courant = (List<ItemPanier>)Session["panier"];
                decimal PrixTot = 0;
                foreach (var item in panier_courant)
                {
                    PrixTot += item.voyage.PrixVenteUnitaire * item.nombre_voyageurs;
                    if (item.assurance != null)
                    {
                        PrixTot += (decimal)item.assurance.Prix;
                    }

                }
                ViewBag.montant = PrixTot;
                return View();

            }
            else return RedirectToAction("Index", "Home");
        }

        public ActionResult Commandes()
        {
            var client = Repo.GetClientByEmail(User.Identity.GetUserName());
            List<Dossier> Dossiers_client = Repo.GetDossierFromClient(client.Id);
            return View(Dossiers_client);
        }

        public ActionResult AnnulerVoyage(Guid? id)
        {
            if (id != null)
            {
                Dossier Doss = Repo.GetDossier(id);//On récupère le dossier et pour chaque voyageur inscrit on supprime le dossiers
                foreach (var v in Doss.Voyageurs)
                {
                    Voyageur vygr = Repo.GetVoyageur(v.Id);
                    vygr.Dossiers.Remove(Doss);
                }
                Repo.SupprimerDossier(id);

            }
            return RedirectToAction("Commandes", "Home");
        }

        public ActionResult Details_Dossier(Guid? id)
        {
            var Doss = Repo.GetDetailsDossier(id);
            return View(Doss);
        }

        public ActionResult SupprimerItem(int index)
        {
            if (Session["panier"] != null)// Si panier est nul, on retourne  à l'accueil
            {
                List<ItemPanier> panier_courant = (List<ItemPanier>)Session["panier"];
                panier_courant.Remove(panier_courant[index]);
                Session["panier"] = panier_courant;
                return RedirectToAction("Panier", "Home");
            }
            return RedirectToAction("Panier", "Home");
        }
    }
}