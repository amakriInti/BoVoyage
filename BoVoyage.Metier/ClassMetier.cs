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
        public bool AddVoyage()
        {
            // Lecture du fichier csv -> ps
            //Récupère tous les fichier .csv du répertoir et le met dans un tableau de string
            string[] files = Directory.GetFiles(@"D:\formation .net\Module C#\DemoASP\SaveVoyage\BoVoyage.Scenario1\Voyage_csv", "*.csv");

            //boucle pour récuperer le fichier spécifique du tableau de string
            for (int i = 0; i < files.Count(); i++)
            {
                //boucle pour intégrer les données du fichier csv dans la BDD
                foreach (var ligne in File.ReadAllLines(files[i]))
                {
                    try
                    {
                        var tab = ligne.ToString().Split(';');
                        bool CtrlChanges = repository.AddVoyage(tab);
                        if (CtrlChanges == false)
                        {
                            throw new Exception();
                        }
                    }
                    catch(Exception ex)
                    {
                        return false;
                    }
                    
                }
                 //supprime un fichier arès l'avoir lu
                File.Delete(files[i]);
            }
            return true;
        }

        /*----------------------------------
        //Afficher liste des voyages
        -----------------------------------*/
        public object GetVoyageFormulaire(string tri, string choix, string choixprecis)
        {
            return repository.GetVoyageFormulaire(tri, choix, choixprecis);
        }
        public object DBVoyages(string tri, string choix)
        {
            return repository.DBVoyages(tri, choix);
        }

        public object DetailsVoyage(string id)
        {
            return repository.DetailsVoyage(id);
        }
        public object DBBanque()
        {
            return repository.DBBanque();
        }

        /*----------------------------------
        //Ajout des voyages depuis formulaire
        -----------------------------------*/
        public bool AddVoyageFormulaire_Metier()
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /*----------------------------------
        //Ajout des clients
        -----------------------------------*/
        public bool AddClient(string nom, string mail, string telephone, string prenom, string personneMorale)
        {
            return repository.AddClient(nom, mail, telephone, prenom, personneMorale);
        }

        /*----------------------------------
        //Initialisation des roles
        -----------------------------------*/
        public void Load()
        {
            droits.Load();
        }
    }
}
