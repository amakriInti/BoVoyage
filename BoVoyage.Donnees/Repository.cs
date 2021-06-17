﻿using System;
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

        /*------------------------------------------
        //Ajouter les voyages dans la base de donnée
        -------------------------------------------*/
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
            catch (Exception)
            {
                return false;
            }
        }

        public object DBVoyages(string tri, string choix)
        {
            var query = (from Voyage in Context.Voyages
                         join DestinationVoyage in Context.DestinationVoyages on Voyage.Id equals DestinationVoyage.Voyage
                         join Destination in Context.Destinations on DestinationVoyage.Destination equals Destination.Id
                         select new VoyageDetail
                         {
                             Id = Voyage.Id,
                             DateAller = Voyage.DateAller,
                             DateRetour = Voyage.DateRetour,
                             MaxVoyageur = Voyage.MaxVoyageur,
                             Fournisseur = Voyage.Fournisseur,
                             PrixAchatTotal = Voyage.PrixAchatTotal,
                             PrixVenteUnitaire = Voyage.PrixVenteUnitaire,
                             Description = Voyage.Description + " " + Destination.Description,
                             Continent = Destination.Continent,
                             Pays = Destination.Pays,
                             Region = Destination.Region
                         });
            //return query.ToList();
            if (tri == "DateAller")
                return query.Where(c => c.DateAller == DateTime.Parse(choix)).ToList();
            else if (tri == "DateRetour")
                return query.Where(c => c.DateRetour == DateTime.Parse(choix)).ToList();
            else if (tri == "MaxVoyageur")
                return query.Where(c => c.MaxVoyageur >= byte.Parse(choix)).ToList();
            else if (tri == "Fournisseur")
                if (choix == "null" || choix == null) return query.Select(c => c.Fournisseur).ToList().Distinct();
                else return query.Where(c => c.Fournisseur == choix).ToList();
            else if (tri == "PrixVenteUnitaire")
                return query.Where(c => c.PrixVenteUnitaire == decimal.Parse(choix)).ToList();
            else if (tri == "Continent")
                if (choix == "null" || choix == null) return query.Select(c => c.Continent).ToList().Distinct();
                else return query.Where(c => c.Continent == choix).ToList();
            else if (tri == "Pays")
                if (choix == "null" || choix == null) return query.Select(c => c.Pays).ToList().Distinct();
                else return query.Where(c => c.Pays == choix).ToList();
            else if (tri == "Region")
                if (choix == "null" || choix == null) return query.Select(c => c.Region).ToList().Distinct();
                else return query.Where(c => c.Region == choix).ToList();
            else if (tri == null)
                return query.ToList();
            else
                return query;
        }

        public object DetailsVoyage(string id)
        {
            var idparsed = Guid.Parse(id);
            try
            {
                var query = (from Voyage in Context.Voyages
                             join DestinationVoyage in Context.DestinationVoyages on Voyage.Id equals DestinationVoyage.Voyage
                             join Destination in Context.Destinations on DestinationVoyage.Destination equals Destination.Id
                             where Voyage.Id == idparsed
                             select new VoyageDetail
                             {
                                 Id = Voyage.Id,
                                 DateAller = Voyage.DateAller,
                                 DateRetour = Voyage.DateRetour,
                                 MaxVoyageur = Voyage.MaxVoyageur,
                                 Fournisseur = Voyage.Fournisseur,
                                 PrixAchatTotal = Voyage.PrixAchatTotal,
                                 PrixVenteUnitaire = Voyage.PrixVenteUnitaire,
                                 Description = Voyage.Description + " " + Destination.Description,
                                 Continent = Destination.Continent,
                                 Pays = Destination.Pays,
                                 Region = Destination.Region
                             });

                return query.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool AddClient(string nom, string mail, string telephone, string prenom, string personneMorale)
        {
            try
            {
                Context.Clients.Add(new Client
                {
                    Id = Guid.NewGuid(),
                    Nom = nom,
                    Prenom = prenom,
                    Mail = mail,
                    Telephone = telephone,
                    PersonneMorale = personneMorale,
                });
                Context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
