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

        //Ajouter les voyages dans la base de donnée
        public bool AddVoyage(string[] tab)
        {
            try
            {
                //DefiningQuery et qu'il n'existe dans l'élément <ModificationFunctionMapping>
                var idDestination = Guid.NewGuid();
                var idVoyage = Guid.NewGuid();

                Context.Voyages.Add(new Voyage
                {
                    Id = idVoyage,
                    DateAller = DateTime.Parse(tab[0]),
                    DateRetour = DateTime.Parse(tab[1]),
                    MaxVoyageur = byte.Parse(tab[2]),
                    Fournisseur = tab[3],
                    PrixAchatTotal = decimal.Parse(tab[4]),
                    PrixVenteUnitaire = decimal.Parse(tab[5]),
                    Description = tab[6]

                });
                Context.SaveChanges();

                Context.Destinations.Add(new Destination
                {
                    Id = idDestination,
                    Continent = tab[7],
                    Pays = tab[8],
                    Region = tab[9],
                    Description = tab[10]
                });
                Context.SaveChanges();

                Context.DestinationVoyages.Add(new DestinationVoyage
                {
                    Voyage=idVoyage,
                    Destination= idDestination
                    
                });
                Context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object DBVoyages()
        {
            var query = from Voyage in Context.Voyages
                        join DestinationVoyage in Context.DestinationVoyages on Voyage.Id equals DestinationVoyage.Voyage
                        join Destination in Context.Destinations on DestinationVoyage.Destination equals Destination.Id
                        select new VoyageDetail { 
                            DateAller = Voyage.DateAller,
                            DateRetour = Voyage.DateRetour,
                            MaxVoyageur = Voyage.MaxVoyageur,
                            Fournisseur = Voyage.Fournisseur,
                            PrixAchatTotal = Voyage.PrixAchatTotal,
                            PrixVenteUnitaire = Voyage.PrixVenteUnitaire,
                            Description = Voyage.Description +" "+ Destination.Description,
                            Continent = Destination.Continent,
                            Pays = Destination.Pays,
                            Region = Destination.Region
                        };

            return query.ToList();
        }
    }

}
