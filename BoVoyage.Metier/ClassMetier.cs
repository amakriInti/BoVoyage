using BoVoyage.Donnees;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage.Metier
{
    public class ClassMetier
    {
        private Repository repository = new Repository();
        private Droits droits = new Droits();

        /*----------------------------------
        //Ajout des voyages depuis fichier CSV
        -----------------------------------*/
        public bool AddVoyageCSV_Metier()
        {
            //Lecture du fichier csv -> ps
            //Récupère tous les fichier .csv du répertoire et le met dans un tableau de string
            //Diego
            //string[] files = Directory.GetFiles(@"C:\Users\user\source\repos\BoVoyage\BoVoyage.Scenario1\Voyage_csv\", "*.csv");

            //Quentin
            string[] files = Directory.GetFiles(@"D:\Utilisateurs\LEFEVRE Quentin\Documents\Cour\Cour_INTI\Projet Fin Formation\Application\BoVoyage.Scenario1\Voyage_csv\", "*.csv");

            //boucle sur les éléments du tableau de fichier
            for (int i = 0; i < files.Count(); i++)
            {
                //Lecture et ajout dans la DB
                foreach (var ligne in File.ReadAllLines(files[i]))
                {
                    try
                    {
                        var tab = ligne.ToString().Split(';');
                        //Ajout dans la DB
                        bool CtrlChanges = repository.AddVoyage(tab);
                        if (CtrlChanges == false)
                        {
                            throw new Exception();
                        }
                    }
                    catch(Exception)
                    {
                        return false;
                    }
                    
                }
                //Suppression du fichier
                File.Delete(files[i]);
            }
            return true;
        }

        /*----------------------------------
        //Ajout des voyages depuis formulaire
        -----------------------------------*/
        //public bool AddVoyageFormulaire_Metier(string fournisseur, DateTime dateDepart, DateTime dateRetour, byte nbPlace, decimal prixAchat, decimal prixVente, string descriptionVoyage, string continent, string pays, string region, string DescriptionDestination)
        public bool AddVoyageFormulaire_Metier(string[] NewVoyage)
        {
            try
            {
                return repository.AddVoyage(NewVoyage);
            }
            catch (Exception)
            {
                return false;
            }
        }
        /*-------------------------------------- Travail de vincent -----------------------------------*/
        /*----------------------------------
        //Ajout des clients
        -----------------------------------
        //Temp
        public Client CreateClient(string nom, string mail, string telephone, string prenom, string personneMorale)
        {
            return repository.CreateClient(nom, mail, telephone, prenom, personneMorale);
        }
        //Dans DB
        public bool AddClient(Client client)
        {
            repository.AddClient(client.Id, client.Nom, client.Prenom, client.Mail, client.PersonneMorale, client.Telephone);
            return true;
        }*/

        /*----------------------------------
        //Ajout des voyageurs
        -----------------------------------
        //temp
        public Voyageur CreateVoyageurs(Voyageur v)
        {
            return repository.CreateVoyageur(v.Nom, v.Prenom, v.DateNaissance, v.IsAccompagnant, v.Mail);
        }
        //Dans DB
        public bool AddVoyageurs(List<Voyageur> voyageurs)
        {
            foreach (Voyageur voyageur in voyageurs)
            {
                repository.AddVoyageurs(voyageur.Id, voyageur.Nom, voyageur.Prenom, voyageur.Mail, voyageur.DateNaissance, voyageur.IsAccompagnant);
            }
            return true;
        }*/

        /*----------------------------------
        //Ajout d'une assurance
        -----------------------------------
        //Temp
        public Assurance CreateAssurance(bool annulation, decimal prix)
        {
            return repository.CreateAssurance(annulation, prix);
        }
        //Dans DB
        public bool AddAssurance(Assurance assurance)
        {
            repository.AddAssurance(assurance.Id, assurance.Annulation, assurance.Prix);
            return true;
        }*/

        /*----------------------------------
        //Ajout d'un dossier
        -----------------------------------
        //Temp
        public Dossier CreateDossier(Guid voyageId, Guid clientId, Guid assuranceId)
        {
            return repository.CreateDossier(voyageId, clientId, assuranceId);
        }
        //Dans DB
        public bool AddDossier(Dossier dossier)
        {
            repository.AddDossier(dossier.Id, dossier.Voyage, dossier.Client, dossier.Assurance, dossier.Commercial, dossier.Etat);
            return true;
        }*/

        /*----------------------------------
        //Ajout d'un dossierVoyageur dans DB
        -----------------------------------
        public bool AddDossierVoyageurs(Dossier dossier, List<Voyageur> voyageurs)
        {
            foreach (Voyageur voyageur in voyageurs)
            {
                repository.AddDossierVoyageurs(dossier.Id, voyageur.Id);
            }
            return true;
        }*/

        /*-------------------------------------- Version new -----------------------------------*/
        /*----------------------------------
        //Ajout des clients
        -----------------------------------*/
        //Dans DB
        public bool AddClient(Guid IdClient, string PersonneMorale, string NomClient, string PrenomClient, string MailClient, string Telephone)
        {
            repository.AddClient(IdClient, PersonneMorale, NomClient, PrenomClient, MailClient, Telephone);
            return true;
        }

        /*----------------------------------
        //Ajout des voyageurs
        -----------------------------------*/
        //Dans DB
        public bool AddVoyageurs(List<string> Voyageurs)
        {
            for(var i=0; i<Voyageurs.Count(); i+=6)
            {
                repository.AddVoyageurs(Guid.Parse(Voyageurs[i]), Voyageurs[i+1], Voyageurs[i+2], DateTime.Parse(Voyageurs[i+3]), bool.Parse(Voyageurs[i+4]), Voyageurs[i+5]);
            }
            return true;
        }

        /*----------------------------------
        //Ajout d'une assurance
        -----------------------------------*/
        //Dans DB
        public bool AddAssurance(Guid IdAssurance, bool Annulation, decimal? Prix)
        {
            repository.AddAssurance(IdAssurance, Annulation, Prix);
            return true;
        }

        /*----------------------------------
        //Ajout d'un dossier
        -----------------------------------*/
        //Dans DB
        public bool AddDossier(Guid IdDossier, Guid IdVoyage, Guid IdClient, byte Etat, Guid IdAssurance, Guid? IdCommercial)
        {
            repository.AddDossier(IdDossier, IdVoyage, IdClient, Etat, IdAssurance, IdCommercial);
            return true;
        }

        /*----------------------------------
        //Ajout d'un dossierVoyageur dans DB
        -----------------------------------*/
        public bool AddDossierVoyageurs(Guid IdDossier, List<string>Voyageurs)
        {
            for (var i = 0; i < Voyageurs.Count(); i += 6)
            {
                repository.AddDossierVoyageurs(IdDossier, Guid.Parse(Voyageurs[i]));
            }
            return true;
        }

        /*----------------------------------
        //Liste des voyages
        -----------------------------------*/
        public object DBVoyages(string tri, string choix)
        {
            return repository.DBVoyages(tri, choix);
        }

        /*----------------------------------
        //Liste pour combobox
        -----------------------------------*/
        public object GetVoyageFormulaire(string tri, string choix, string choixprecis)
        {
            return repository.GetVoyageFormulaire(tri, choix, choixprecis);
        }

        /*----------------------------------
        //Détail des voyages
        -----------------------------------*/
        public object DetailsVoyage(string id)
        {
            return repository.DetailsVoyage(id);
        }

        /*----------------------------------
        //Devis
        -----------------------------------*/
        public object Devis(string id)
        {
            return repository.Devis(id);
        }

        /*----------------------------------
        //Initialisation des roles
        -----------------------------------*/
        public void Load()
        {
            repository.LoadDroits();
        }

        /*----------------------------------
        //Liste des commerciaux (à vérifier)
        -----------------------------------*/
        public List<string> GetLoginCommerciaux()
        {
            return repository.GetCommerciaux().Select(c => c.Login).ToList();
        }

        /*----------------------------------
        //Liste des dossiers (à vérifier)
        -----------------------------------*/
        public object GetDossiers()
        {
            return repository.GetDossiers();
        }
    }
}
