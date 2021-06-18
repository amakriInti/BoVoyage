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
            try
            {
                string[] files = Directory.GetFiles(path_csv);
                foreach (string f in files)
                {
                    if (LectureCsv(f) != null)
                    {
                        var Vygs = LectureCsv(f); //Lit le fichier CSV
                        Context.Voyages.AddRange(Vygs);//Ajoute à la base de données
                        File.Delete(f);//Efface le fichier 
                    }
                    else return;
                }
                Context.SaveChanges();//sauvegarde les changements 
            }
            catch (Exception)
            {
                return;
            }
        }

        private static List<Voyage> LectureCsv(string path)//fonctions de lecture du fichier csv
        {
            List<Voyage> vy = new List<Voyage>();
            try
            {
                string[] file_reader = File.ReadAllLines(path);
                foreach (var ligne in file_reader)
                {
                    var tab = ligne.Split(';');
                    vy.Add(new Voyage { Id = Guid.NewGuid(), Fournisseur = tab[0], Libelle = tab[1], DateAller = Convert.ToDateTime(tab[2]), DateRetour = Convert.ToDateTime(tab[3]), MaxVoyageurs = Convert.ToByte(tab[4]), PrixAchatTotal = Convert.ToDecimal(tab[5]), Description = tab[6] });
                }
                return vy;
            }

            catch(Exception)
            {
                return null;
            }
            // Lecture du fichier csv -> ps

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
                Context.Dossiers.Where(d => d.Id == id_doss).FirstOrDefault().Etat = 3 ;
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        internal bool DossierPaye(Guid? id_doss)
        {
            if (id_doss != null)
            {
                Context.Dossiers.Where(d => d.Id == id_doss).FirstOrDefault().Etat = 1;
                Context.SaveChanges();//ça marche
                return true;
            }
            else return false;
        }

        internal bool RefuserDossier(Guid? id_doss)
        {
            if (id_doss != null)
            {
                Context.Dossiers.Where(d => d.Id == id_doss).FirstOrDefault().Etat = 2;
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

        internal Dossier GetDetailsDossier(Guid? id_doss)
        {
            if (id_doss != null)
            {
                return Context.Dossiers.Where(d => d.Id == id_doss).FirstOrDefault();
            }
            else return null;
        }

        internal Voyage GetVoyage(Guid? id)
        {
            if (id != null)
            {
                return Context.Voyages.Where(d => d.Id == id).FirstOrDefault();
            }
            else return null;
        }

        internal List<Voyage> GetAllVoyages()
        {
            return Context.Voyages.ToList();
        }


        internal List<Assurance> GetAllAssurances()
        {
            return Context.Assurances.ToList();
        }

        internal Assurance GetAssurance(Guid? id)
        {
            if (id != null)
            {
                return Context.Assurances.Where(d => d.Id == id).FirstOrDefault();
            }
            else return null;
        }

        internal Dossier GetDossier(Guid? id)
        {
            if (id != null)
            {
                return Context.Dossiers.Where(d => d.Id == id).FirstOrDefault();
            }
            else return null;
        }


        internal List<Dossier> GetDossierFromClient(Guid? id)
        {
            if (id != null)
            {
                return Context.Dossiers.Where(d => d.Client == id).ToList();
            }
            else return null;
        }



        internal Guid? NouveauDossier(string email,Guid? id_voyage)
        {
            if (id_voyage != null)
            {
                Guid? Id_dossier = Guid.NewGuid();
                var client = Context.Clients.Where(c => c.email == email).FirstOrDefault();
                if (client==null)
                {
                    return null;
                }
                Context.Dossiers.Add(new Dossier { Id = (Guid)Id_dossier, Client = client.Id, Voyage = (Guid)id_voyage });
                Context.SaveChanges();
                return Id_dossier;
            }
            else return null;
        }

        internal bool SupprimerDossier(Guid? id)
        {
            if (id != null)
            {
                Dossier doss = Context.Dossiers.Where(d => d.Id == id).FirstOrDefault();
                Context.Dossiers.Remove(doss);
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        internal bool NouveauClient(string email, string nom, string prenom)
        {
            if (email != null)
            { 
                Context.Clients.Add(new Client { Id = Guid.NewGuid(), email = email, Prenom = prenom, Nom =nom });
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        internal Client GetClientByEmail(string email)
        {
            return Context.Clients.Where(c => c.email == email).FirstOrDefault();
        }
        internal bool AddVoyageur(Voyageur vygr)
        {
            if (vygr != null)
            {
                Context.Voyageurs.Add(vygr);
                Context.SaveChanges();
                return true;
            }
            else return false;
        }

        internal Voyageur GetVoyageur(Guid? id)
        {
            if (id != null)
            {
                return Context.Voyageurs.Where(d => d.Id == id).FirstOrDefault();
            }
            else return null;
        }

    }
}