using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BoVoyage.Scenario1.Models
{
    public class TraitementBD
    {
        private BoVoyageContext Context = new BoVoyageContext();

        public int MaxVoyageur
        {
            get
            {
                var res = Context.Voyages.Select(v => v.MaxVoyageur).FirstOrDefault();
                return res;
            }
        }
        public string DateAller
        {
            get
            {
                var res = Context.Voyages.Select(v => v.DateAller).FirstOrDefault();
                
                string jour = res.Day.ToString();
                string mois = res.Month.ToString();
                string annee = res.Year.ToString();

                if (!jour.StartsWith("0") && int.Parse(jour)<=9)
                {
                    jour = $"0{jour}";
                }
                if (!mois.StartsWith("0") && int.Parse(mois) <= 9)
                {
                    mois = $"0{mois}";
                }

                string date = $"{jour}/{mois}/{annee}";
                return date;
            }
        }
        public string DateRetour
        {
            get
            {
                var res = Context.Voyages.Select(v => v.DateRetour).FirstOrDefault();

                string jour = res.Day.ToString();
                string mois = res.Month.ToString();
                string annee = res.Year.ToString();

                if (!jour.StartsWith("0") && int.Parse(jour) <= 9)
                {
                    jour = $"0{jour}";
                }
                if (!mois.StartsWith("0") && int.Parse(mois) <= 9)
                {
                    mois = $"0{mois}";
                }

                string date = $"{jour}/{mois}/{annee}";
                return date;
            }
        }
        public decimal Prix
        {
            get
            {
                var res = Context.Voyages.Select(v => v.PrixVenteUnitaire).FirstOrDefault();
                return res;
            }
        }
        public string Description
        {
            get
            {
                var res = Context.Voyages.Select(v => v.Description).FirstOrDefault();
                return res;
            }
        }
    }
}