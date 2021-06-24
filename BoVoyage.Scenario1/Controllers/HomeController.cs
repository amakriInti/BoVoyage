using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.IO;
using System;
using BoVoyage.Metier;
//using BoVoyage.Donnees; //(à supprimer après modif vincent)
using System.Collections;
using BoVoyage.Scenario1.Models;

namespace BoVoyage.Scenario1.Controllers
{
    public class HomeController : Controller
    {
        private ClassMetier metier = new ClassMetier();
        private Panier panier;


        /*-------------------------
        //Constructeur initialisation du panier
        --------------------------*/
        public HomeController()
        {
            panier = new Panier();
            //Travail Vincent
            //panier.Voyageurs = new List<Voyageur>();
        }

        public ActionResult Index()
        {
            //Réinitialisation de l'objet session
            Session.Clear();

            return View();
        }



        /* -----------------Travail de vincent --------------------------------------------------*/
        #region
        /*-------------------------
        //Formulaire client
        --------------------------
        public ActionResult InformationClient(Guid IdVoyage)
        {
            panier.IdVoyage = IdVoyage;
            Session["panier"] = panier;
            return View();
        }

        /*-------------------------
        //Formulaire participant affichage + envoi formulaire client (httppost)
        --------------------------
        public ActionResult Participant(string nom, string mail, string telephone, string prenom, string personneMorale)
        {
            if (Session["panier"] != null) { panier = (Panier)Session["panier"]; }
            panier.Client = metier.CreateClient(nom, mail, telephone, prenom, personneMorale);
            Session["panier"] = panier;
            return View();

        }

        /*-------------------------
        //Formulaire participant envoi 
        --------------------------*/
        /*[HttpPost]
        public ActionResult Participant(List<Voyageur> voyageurs)
        {
            panier = (Panier)Session["panier"];
            foreach (var voyageur in voyageurs)
            {

                panier.Voyageurs.Add(metier.CreateVoyageurs(voyageur));
            }
            Session["panier"] = panier;
            return View();
        }*/

        /*-------------------------
        //Formulaire assurance affichage
        --------------------------
        public ActionResult Assurance()
        {
            return View();
        }

        /*-------------------------
        //Formulaire assurance envoie (à mettre en httpPost)
        --------------------------
        public ActionResult ValideAssurance(bool assurance)
        {
            panier = (Panier)Session["panier"];
            decimal prix = 100;
            panier.Assurance = metier.CreateAssurance(assurance, prix);
            panier.Dossier = metier.CreateDossier(panier.IdVoyage, panier.Client.Id, panier.Assurance.Id);
            Session["panier"] = panier;
            
            return RedirectToAction("Recap");
        }

        /*-------------------------
        //Récapitulatif de commande
        --------------------------
        public ActionResult Recap()
        {
            return View();
        }

        /*-----------------------
        //Vue paiement (à faire)
        ------------------------
        public ActionResult Paiement()
        {
            return View();
        }

        /*-------------------------
        //Validation paiement
        --------------------------
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
        }*/
        #endregion

        /*----------------------------Version new------------------------------
        /*-------------------------
        //Formulaire client affichage
        --------------------------*/
        public ActionResult InformationClient(Guid IdVoyage)
        {
            panier.IdVoyage = IdVoyage;
            Session["panier"] = panier;
            return View();
        }

        /*-------------------------
        //Formulaire client envoi
        --------------------------*/
        [HttpPost]
        public ActionResult InformationClient(string id)
        {
            //Vérification création panier
            if (Session["panier"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //Récupération des données du formulaire
                string entreprise = Request.Form["Entreprise"];
                string personneMorale = null;
                if (entreprise == "true")
                {
                    personneMorale = "Société";
                }
                else
                {
                    personneMorale = Request.Form["Civilite"];
                }
                string nom = Request.Form["Nom"];
                string prenom = Request.Form["Prenom"];
                string mail = Request.Form["Mail"];
                string telephone = Request.Form["Telephone"];

                //Appel du panier
                panier = (Panier)Session["panier"];

                //Attribution dans l'objet panier
                panier.IdClient = Guid.NewGuid();
                panier.PersonneMorale = personneMorale;
                panier.NomClient = nom;
                panier.PrenomClient = prenom;
                panier.MailClient = mail;
                panier.Telephone = telephone;

                //Enregistrement du panier
                Session["panier"] = panier;

                return RedirectToAction("Participant");
            }
        }

        /*-------------------------
        //Formulaire participant affichage
        --------------------------*/
        public ActionResult Participant()
        {
            if (Session["panier"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        /*-------------------------
        //Formulaire participant envoi
        --------------------------*/
        [HttpPost]
        public ActionResult Participant(string id)
        {
            //Vérification création panier
            if (Session["panier"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //Appel du panier
                panier = (Panier)Session["panier"];
                List<VoyageursPanier> voyageurs = new List<VoyageursPanier>();

                //Récupéré données du formaulaire et les ajouter à la liste
                var NbParticipant = int.Parse(Request.Form["NbParticipant"]);
                for (var i = 0; i < NbParticipant; i++)
                {
                    string NomVoyageur = Request.Form["Participant[" + i + "].Nom"];
                    string PrenomVoyageur = Request.Form["Participant[" + i + "].Prenom"];
                    DateTime DateDeNaissance = DateTime.Parse(Request.Form["Participant[" + i + "].DateDeNaissance"]);
                    string MailVoyageur = Request.Form["Participant[" + i + "].Mail"];

                    string AccompagnantStr = Request.Form["Participant[" + i + "].Accompagnant"];
                    bool Accompagnant = false;
                    if (AccompagnantStr == "true")
                    {
                        Accompagnant = true;
                    }
                    else
                    {
                        Accompagnant = false;
                    }

                    voyageurs.Add(
                        new VoyageursPanier
                        {
                            IdVoyageur = Guid.NewGuid(),
                            NomVoyageur = NomVoyageur,
                            PrenomVoyageur = PrenomVoyageur,
                            DateDeNaissance = DateDeNaissance,
                            MailVoyageur = MailVoyageur,
                            Accompagnant = Accompagnant
                        }
                    );
                }

                //Attribution dans l'objet panier
                panier.VoyageurPanier = voyageurs;

                //Enregistrement du panier
                Session["panier"] = panier;

                return RedirectToAction("Assurance");
            }
        }

        /*-------------------------
        //Formulaire assurance affichage
        --------------------------*/
        public ActionResult Assurance()
        {
            if (Session["panier"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        /*-------------------------
        //Formulaire assurance envoi
        --------------------------*/
        [HttpPost]
        public ActionResult Assurance(string id)
        {
            //Vérification création panier
            if (Session["panier"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //Récupération des données du formulaire
                string AssuAnnu = Request.Form["AssuranceAnnu"];
                bool assurance = false;
                if (AssuAnnu=="True")
                {
                    assurance = true;
                }
                else if (AssuAnnu=="False")
                {
                    assurance = false;
                }

                //Appel du panier
                panier = (Panier)Session["panier"];

                //Attribution dans l'objet panier de l'assurrance
                panier.IdAssurance = Guid.NewGuid();
                panier.Annulation = assurance;
                if (assurance == true)
                {
                    panier.Prix = 100;
                }
                else
                {
                    panier.Prix = 0;
                }

                //Attribution dans l'objet panier du dossier
                panier.IdDossier = Guid.NewGuid();
                panier.Etat = (byte)Etat.EnAttente;
                panier.IdCommercial = null;

                //Enregistrement du panier
                Session["panier"] = panier;

                return RedirectToAction("Recap");
            }
        }

        /*-------------------------
        //Récapitulatif de commande
        --------------------------*/
        public ActionResult Recap()
        {
            if (Session["panier"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        /*-----------------------
        //Vue paiement (à faire)
        ------------------------*/
        public ActionResult Paiement()
        {
            if (Session["panier"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Paiement(string id)
        {
            //Envoie et vérification des données de paiement
            /*-------------- A faire -------------------*/

            //Vérification création panier
            if (Session["panier"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //Appel du panier
                panier = (Panier)Session["panier"];

                //Transformation liste en liste de string
                List<string> VoyageursString = new List<string>();
                for (var i = 0; i < panier.VoyageurPanier.Count(); i++)
                {
                    VoyageursString.Add(panier.VoyageurPanier[i].IdVoyageur.ToString());
                    VoyageursString.Add(panier.VoyageurPanier[i].NomVoyageur.ToString());
                    VoyageursString.Add(panier.VoyageurPanier[i].PrenomVoyageur.ToString());
                    VoyageursString.Add(panier.VoyageurPanier[i].DateDeNaissance.ToString());
                    VoyageursString.Add(panier.VoyageurPanier[i].Accompagnant.ToString());
                    VoyageursString.Add(panier.VoyageurPanier[i].MailVoyageur.ToString());
                }

                

                //Appel des méthode pour ajout dans la DB depuis le panier
                metier.AddClient(panier.IdClient, panier.PersonneMorale, panier.NomClient, panier.PrenomClient, panier.MailClient, panier.Telephone);
                metier.AddVoyageurs(VoyageursString);
                metier.AddAssurance(panier.IdAssurance, panier.Annulation, panier.Prix);
                metier.AddDossier(panier.IdDossier, panier.IdVoyage, panier.IdClient, panier.Etat, panier.IdAssurance, panier.IdCommercial);
                metier.AddDossierVoyageurs(panier.IdDossier, VoyageursString);

                //Réinitialisation de l'objet
                Session.Clear();

                return RedirectToAction("Validation");
            }
        }

        /*-------------------------
        //Validation paiement
        --------------------------*/
        public ActionResult Validation()
        {
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

        /*----------------------------------
        //Affichage du devis
        -----------------------------------*/
        public ActionResult Devis(string id)
        {
            if (id == null) return RedirectToAction("Index");
            var devis = metier.DetailsVoyage(id);
            if (devis == null) return RedirectToAction("Index");
            return View(devis);
        }

        /*-------------------------
        //Récapitulatif après commande
        --------------------------*/
        public ActionResult RecapDossier()
        {
            return View();
        }
    }

    /*----------------------------------
    //Classe enregistrement des données temp du panier
    -----------------------------------*/
    /*public class Panier
    {
        public Guid IdVoyage { get; set; }
        public Dossier Dossier { get; set; }
        public Assurance Assurance { get; set; }
        public Client Client { get; set; }
        public List<Voyageur> Voyageurs { get; set; }
    }*/
}