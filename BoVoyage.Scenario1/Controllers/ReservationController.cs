using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BoVoyage.Scenario1.Dal;

namespace BoVoyage.Scenario1.Controllers
{
    public class ReservationController : Controller
    {
        public static Guid id_voyage; //Comme ça l'id du voyage persiste jusqu'à ce qu'on ait validé
        public static int nb_voyageurs; //
        public static Guid id_assurance;
        // GET: Reservation
        [Authorize]
        public ActionResult Reserver(Guid? id)
        {
            if (id != null)
            {
                Repository Repo = new Repository();
                id_voyage = (Guid)id;
                ViewBag.Assurances = Repo.GetAllAssurances();
                Voyage Vyg = Repo.GetVoyage(id_voyage);//permet d'afficher le bon libellé dans le formulaire 
                return View(Vyg);
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Reserver(int? nombre_voyageurs)//Permet de réserver le voyage avec le bon nombre de passagers
        {
            if (nombre_voyageurs != null)
            {
                if (Guid.TryParse(System.Web.HttpContext.Current.Request.Form["Assurance"], out Guid id_asr))//s'assure que l'assurance n'est pas null
                {
                    id_assurance = id_asr;
                }
                nb_voyageurs = Convert.ToInt32(nombre_voyageurs);
                return RedirectToAction("EntrerVoyageurs", "Reservation");
            }
            else return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult EntrerVoyageurs()//Formulaire
        {
            if (nb_voyageurs != 0)
            {
                Repository Repo = new Repository();
                List<Voyageur> Vygrs = new List<Voyageur>();
                for (int i = 1; i <= nb_voyageurs; i++)
                {
                    Vygrs.Add(new Voyageur());
                }
                ViewBag.nombre_voyageurs = nb_voyageurs;
                return View(Vygrs);
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public ActionResult EntrerVoyageurs(int nb_vg)//Récupère l'id du voyage choisi
        {
            if (nb_voyageurs != 0)
            {
                Repository Repo = new Repository();// On ne remplit le panier que si on a tous les voyageurs
             //--------------Update Panier----------------------------------------------------------                                                 
                if (Session["panier"] == null)//Si le panier existe pas on le crée sinon on le récupère
                { Session["panier"] = new List<ItemPanier>(); }
                List<ItemPanier> panier_courant = (List<ItemPanier>)Session["panier"];//récupère le panier
                if (Repo.NouveauDossier(User.Identity.GetUserName(), id_voyage) != null)//
                {
                    Guid id_dossier = (Guid)Repo.NouveauDossier(User.Identity.GetUserName(), id_voyage);
                    panier_courant.Add(new ItemPanier { 
                        voyage = Repo.GetVoyage(id_voyage), 
                        nombre_voyageurs = nb_voyageurs, 
                        id_dossier = id_dossier,
                        assurance = Repo.GetAssurance(id_assurance) });
                    Session["panier"] = panier_courant;
                    //--------------Update Panier-----------------------------------------------------------

                    //--------------Création Dossier et Ajout Voyageurs-----------------------------------------------------------
                    for (int i = 0; i < nb_voyageurs; i++)
                    {
                        Guid Id_nouveau_voyageur = Guid.NewGuid();
                        Voyageur Vygr = new Voyageur
                        {
                            Id = Id_nouveau_voyageur,
                            Nom = System.Web.HttpContext.Current.Request.Form["name_" + i],
                            Prenom = System.Web.HttpContext.Current.Request.Form["fname_" + i],
                            DateNaissance = Convert.ToDateTime(System.Web.HttpContext.Current.Request.Form["date_" + i]),
                            IsAccompagnant = Convert.ToBoolean(System.Web.HttpContext.Current.Request.Form["acc_" + i]),
                        };
                        Vygr.Dossiers.Add(Repo.GetDossier(id_dossier));// crée le lien dans la table secondaire
                        Repo.AddVoyageur(Vygr);
                        //--------------Création Dossier et Ajout Voyageurs-----------------------------------------------------------
                        //Une fois les voyageurs ajoutés On met le voyage au panier

                    }

                    return RedirectToAction("Confirmation", "Home");// va à confirmation... on ne crée pas de dossier pour l'instant
                }
                else//je suis loggé mais pas client
                {
                    return RedirectToAction("Login2", "Reservation");
                }
                
            }

            else return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Login2() //Sert à enregistrer un utilisateur déjà connu mais non client
        {
            return View((object)User.Identity.GetUserName());
        }

        [HttpPost]
        [Authorize]
        public ActionResult Login2(string LoginClient)
        {
            if(LoginClient!=null)
            {
                Repository Repo = new Repository();
                Repo.NouveauClient(LoginClient, System.Web.HttpContext.Current.Request.Form["name"], System.Web.HttpContext.Current.Request.Form["fname"]);
            }
            return RedirectToAction("Index", "Home");

        }
    }
}