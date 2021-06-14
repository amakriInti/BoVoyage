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
        public bool AddVoyage()
        {
            // Lecture du fichier csv -> ps
            //Récupère tous les fichier .csv du répertoir et le met dans un tableau de string
            string[] files = Directory.GetFiles(@"D:\Utilisateurs\LEFEVRE Quentin\Documents\Cour\Cour_INTI\Projet Fin Formation\Application\BoVoyage.Scenario1\Voyage_csv\", "*.csv");

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
                    catch(Exception)
                    {
                        return false;
                    }
                    
                }
                //supprime un fichier arès l'avoir lu
                File.Delete(files[i]);
            }
            return true;
        }

        public object DBVoyages()
        {
            return repository.DBVoyages();
        }

        private Droits droits = new Droits();
        /*----------------------------------
        //Ajout des voyages depuis fichier CSV
        -----------------------------------*/
        public bool AddVoyage()
        {
            //Lecture du fichier csv -> ps
            //Récupère tous les fichier .csv du répertoire et le met dans un tableau de string
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
        //Liste des voyages
        -----------------------------------*/
        public object DBVoyages()
        {
            return repository.DBVoyages();
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
