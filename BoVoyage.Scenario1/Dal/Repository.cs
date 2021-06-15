using BoVoyage.Scenario1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BoVoyage.Scenario1.Dal
{
    public class Repository
    {
        private static string path_csv = "C:\\Users\\Matthieu\\OneDrive\\Documents\\BoVoyageCShark\\BoVoyage.Scenario1\\Dal\\BoVoyage.csv";
        private static BoVoyageContext Context = new BoVoyageContext();


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
            List<Voyage> ps = new List<Voyage>();

            foreach (var ligne in File.ReadAllLines(path_csv))
            {
                var tab = ligne.Split(';');
                ps.Add(new Voyage { Id = Guid.NewGuid(), Agence = tab[0], Libelle = tab[1], DateAller = Convert.ToDateTime(tab[2]), DateRetour = Convert.ToDateTime(tab[3]), MaxVoyageur = Convert.ToByte(tab[4]), PrixVenteUnitaire = Convert.ToDecimal(tab[5]), Description = tab[6] });
            }

            return ps;
        }
    }
}