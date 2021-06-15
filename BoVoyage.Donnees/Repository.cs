using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage.Donnees
{
    public class Repository
    {
        private BoVoyageContext1 Context = new BoVoyageContext1();
        internal List<string> GetAllMails(StatutEnum statut)
        {
            var liste = Context.Employes.ToList();
            return liste
                .Where(e => (StatutEnum)e.Statut == statut)
                .Select(e => e.Login)
                .ToList();
        }

        /*----------------------------------
        //Ajout des voyages dans la DB
        -----------------------------------*/
        public bool AddVoyage(string[] tab)
        {
            try
            {
                Context.Voyages.Add(new Voyage
                {
                    Id = Guid.NewGuid(),
                    DateAller = DateTime.Parse(tab[0]),
                    DateRetour = DateTime.Parse(tab[1]),
                    MaxVoyageur = byte.Parse(tab[2]),
                    Fournisseur = Guid.NewGuid(),
                    PrixAchatTotal = decimal.Parse(tab[4]),
                    PrixVenteUnitaire = decimal.Parse(tab[5]),
                    Description = tab[6]
                });
                Context.SaveChanges();

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
            return Context.Voyages.ToList();
        }
    }
}
