using BoVoyage.Scenario1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyage.Scenario1.Dal
{
    public class Repository
    {
        private BoVoyageContext Context = new BoVoyageContext();
        internal List<string> GetAllMails(StatutEnum statut)
        {
            var liste = Context.Employes.ToList();
            return liste
                .Where(e => (StatutEnum)e.Statut == statut)
                .Select(e => e.Login)
                .ToList();
        }
    }
}