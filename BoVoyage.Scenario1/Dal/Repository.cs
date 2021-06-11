using BoVoyage.Scenario1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
//haha
namespace BoVoyage.Scenario1.Dal
{
    public class Repository
    {
        private static string path_csv = "C:\\Users\\INTI\\source\\repos\\BoVoyageGit\\BoVoyage.Scenario1\\Voyages.csv";
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
            var Vygs = LectureCsv();
            Context.Voyages.AddRange(Vygs);
            Context.SaveChanges();
        }

        private static List<Voyage> LectureCsv()
        {
            List<Voyage> vy = new List<Voyage>();

            // Lecture du fichier csv -> ps
            foreach (var ligne in File.ReadAllLines(path_csv))
            {
                var tab = ligne.Split(';');
                vy.Add(new Voyage { Id = Guid.NewGuid(), Fournisseur = tab[0], Libelle = tab[1], DateAller = Convert.ToDateTime(tab[2]), DateRetour = Convert.ToDateTime(tab[3]), MaxVoyageurs = Convert.ToByte(tab[4]), PrixAchatTotal = Convert.ToDecimal(tab[5]), Description = tab[6] });
            }
            return vy;
        }
    }
}