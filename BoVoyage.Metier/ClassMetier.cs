﻿using BoVoyage.Donnees;
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
        //Ajout des clients
        -----------------------------------*/
        public bool AddClient(string nom, string mail, string telephone, string prenom, string personneMorale)
        {
            return repository.AddClient(nom, mail, telephone, prenom, personneMorale);
        }

        /*----------------------------------
        //Ajout des voyageurs
        -----------------------------------*/
        public bool AddVoyageurs(string nom, string prenom, DateTime naissance, bool isAccompagnant, string mail)
        {
            return repository.AddVoyageur(nom, prenom, naissance, isAccompagnant, mail);
        }

        /*----------------------------------
        //Liste des voyages
        -----------------------------------*/
        public object DBVoyages(string tri, string choix)
        {
            return repository.DBVoyages(tri, choix);
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
