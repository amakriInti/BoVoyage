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
