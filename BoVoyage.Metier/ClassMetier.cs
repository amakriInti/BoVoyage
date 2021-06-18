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

        /*----------------------------------
        //Ajout des clients
        -----------------------------------*/
        public Guid AddClient(string nom, string mail, string telephone, string prenom, string personneMorale)
        {
            return repository.AddClient(nom, mail, telephone, prenom, personneMorale);
        }

        /*----------------------------------
        //Ajout des voyageurs
        -----------------------------------*/
        public Guid AddVoyageurs(string nom, string prenom, DateTime naissance, bool isAccompagnant, string mail)
        {
            return repository.AddVoyageur(nom, prenom, naissance, isAccompagnant, mail);
        }

        /*----------------------------------
        //Ajout d'une assurance
        -----------------------------------*/
        public Guid CreateAssurance(bool annulation, decimal prix)
        {
            return repository.CreateAssurance(annulation, prix);
        }

        /*----------------------------------
        //Ajout d'un dossier
        -----------------------------------*/
        public Guid CreateDossier(Guid voyageId, Guid clientId, Guid assuranceId, Guid commercialId)
        {
            return repository.CreateDossier( voyageId, clientId, assuranceId, commercialId);
        }

        /*----------------------------------
        //Ajout d'un dossierVoyageur
        -----------------------------------*/
        public bool CreateDossierVoyageur(Guid dossierId, Guid voyageurId)
        {
            return repository.CreateDossierVoyageur(dossierId, voyageurId);
        }

        /*----------------------------------
        //Liste des voyages
        -----------------------------------*/
        public object DBVoyages(string tri, string choix)
        {
            return repository.DBVoyages(tri, choix);
        }

        /*----------------------------------
        //Détail des voyages
        -----------------------------------*/
        public object DetailsVoyage(string id)
        {
            return repository.DetailsVoyage(id);
        }
        /*----------------------------------
        //Initialisation des roles
        -----------------------------------*/
        public void Load()
        {
            repository.LoadDroits();
        }
        /*----------------------------------
        //Liste des commerciaux
        -----------------------------------*/
        public List<string> GetLoginCommerciaux()
        {
            return repository.GetCommerciaux().Select(c => c.Login).ToList();
        }

        /*----------------------------------
        //Liste des dossiers
        -----------------------------------*/
        public object GetDossiers()
        {
            return repository.GetDossiers();
        }
    }
}
