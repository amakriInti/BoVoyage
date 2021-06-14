using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage.Donnees
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
