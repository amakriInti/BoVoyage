using BoVoyage.Scenario1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Microsoft.AspNet.Identity;
using System.Web.Security;

//haha
namespace BoVoyage.Scenario1.Dal
{
    public class Repository
    {
        private static string path_csv = "C:\\Users\\INTI\\source\\repos\\BoVoyageGit\\BoVoyage.Scenario1\\Voyages";
        private Model1 Context = new Model1();
        internal List<string> GetAllMails(StatutEnum statut)
        {
            var liste = Context.Employes.ToList();
            return liste
                .Where(e => (StatutEnum)e.Statut == statut)
                .Select(e => e.Login)
                .ToList();
        }

        internal void ReadCsv()
        {
           string [] files = Directory.GetFiles(path_csv);
            foreach (string f in files)
            {
                var Vygs = LectureCsv(f); //Lit le fichier CSV
                Context.Voyages.AddRange(Vygs);//Ajoute à la base de données
                File.Delete(f);//Efface le fichier 
            }
            Context.SaveChanges();//sauvegarde les changements 
        }

        private static List<Voyage> LectureCsv(string path)//fonctions de lecture du fichier csv
        {
            List<Voyage> vy = new List<Voyage>();

            // Lecture du fichier csv -> ps
            foreach (var ligne in File.ReadAllLines(path))
            {
                var tab = ligne.Split(';');
                vy.Add(new Voyage { Id = Guid.NewGuid(), Fournisseur = tab[0], Libelle = tab[1], DateAller = Convert.ToDateTime(tab[2]), DateRetour = Convert.ToDateTime(tab[3]), MaxVoyageurs = Convert.ToByte(tab[4]), PrixAchatTotal = Convert.ToDecimal(tab[5]), Description = tab[6] });
            }
            return vy;
        }

        internal List<Dossier> GetAllDossiers()//retourne la liste des dossiers
        {
            var liste = Context.Dossiers.ToList();
            return liste;
        }

        internal List<List<Dossier>> GetAllDossiersEmp(string email)//retourne la liste des dossiers
        {
            var liste_doss = new List<List<Dossier>>();
            var employe = Context.Employes.Where(e => e.Login == email).FirstOrDefault();
            var liste_comm = Context.Dossiers.Where(l => l.Commercial == employe.Id).ToList();
            var liste_comm_bar = Context.Dossiers.Where(l => l.Commercial != employe.Id).ToList();
            var sorted_liste_comm = liste_comm.OrderBy(x => x.Etat).ToList();
            var sorted_liste_comm_bar = liste_comm_bar.OrderBy(x => x.Etat).ToList();//On trie les dossier par état pour mettre les non traités en haut
            liste_doss.Add(sorted_liste_comm);
            liste_doss.Add(sorted_liste_comm_bar);
            return liste_doss;
        }


        internal bool ChangeCommercial(Guid? id_doss,string email)//Change le commercial du dossier sur l'utilisateur connecté
        {
            var employe = Context.Employes.Where(e => e.Login == email).FirstOrDefault();
            if (employe.Statut == 1)
            {
                Context.Dossiers.Where(d => d.Id == id_doss).FirstOrDefault().Commercial = employe.Id;
                Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool ValiderDossier(Guid? id_doss)
        {
            if (id_doss != null)
            {
                Context.Dossiers.Where(d => d.Id == id_doss).FirstOrDefault().Etat = 2;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        internal bool RefuserDossier(Guid? id_doss)
        {
            if (id_doss != null)
            {
                Context.Dossiers.Where(d => d.Id == id_doss).FirstOrDefault().Etat = 1;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        internal bool AbandonnerDossier(Guid? id_doss)
        {
            if (id_doss != null)
            {
                Context.Dossiers.Where(d => d.Id == id_doss).FirstOrDefault().Commercial =null;
                Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}