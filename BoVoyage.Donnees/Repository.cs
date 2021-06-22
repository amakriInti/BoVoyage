using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace BoVoyage.Donnees
{
    public class Repository
    {
        private BoVoyageContext Context = new BoVoyageContext();
        private Droits droits = new Droits();
        internal List<string> GetAllMails(StatutEnum statut)
        {
            var liste = Context.Employes.ToList();
            return liste
                .Where(e => (StatutEnum)e.Statut == statut)
                .Select(e => e.Login)
                .ToList();
        }

        /*------------------------------------------
        //Ajout de d'employés dans la base de donnée
        -------------------------------------------*/
        public Guid AddEmploye(string login, string mdp, StatutEnum statut)
        {
            Guid employeId = new Guid();
            Context.Employes.Add(new Employe {
                Id = employeId,
                Login = login,
                MotDePasse = mdp,
                Statut = (byte)statut
            });
            return employeId;
        }
        //1
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

        public IQueryable<Employe> GetCommerciaux()
        {
            return Context.Employes.Where(e => e.Statut == (byte)StatutEnum.Commercial);
        }

        public object DBVoyages(string tri, string choix)
        {
            var query = (from Voyage in Context.Voyages
                        join DestinationVoyage in Context.DestinationVoyages on Voyage.Id equals DestinationVoyage.Voyage
                        join Destination in Context.Destinations on DestinationVoyage.Destination equals Destination.Id
                        select new VoyageDetail {
                            Id = Voyage.Id,
                            DateAller = Voyage.DateAller,
                            DateRetour = Voyage.DateRetour,
                            MaxVoyageur = Voyage.MaxVoyageur,
                            Fournisseur = Voyage.Fournisseur,
                            PrixAchatTotal = Voyage.PrixAchatTotal,
                            PrixVenteUnitaire = Voyage.PrixVenteUnitaire,
                            DescriptionVoyage = Voyage.Description,
                            DescriptionDestination = Destination.Description,
                            Continent = Destination.Continent,
                            Pays = Destination.Pays,
                            Region = Destination.Region,
                            Image = Voyage.Image
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
                                 DescriptionVoyage = Voyage.Description,
                                 DescriptionDestination = Destination.Description,
                                 Continent = Destination.Continent,
                                 Pays = Destination.Pays,
                                 Region = Destination.Region,
                                 Image = Voyage.Image
                             });

                return query.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public object Devis(string id)
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
                                 DescriptionVoyage = Voyage.Description,
                                 DescriptionDestination = Destination.Description,
                                 Continent = Destination.Continent,
                                 Pays = Destination.Pays,
                                 Region = Destination.Region,
                                 Image = Voyage.Image
                             });

                return query.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Guid AddClient(string nom, string mail, string telephone, string prenom, string personneMorale)
        {
            Guid IdClient = Guid.NewGuid();
            Context.Clients.Add(new Client
            {
                Id = IdClient,
                Nom = nom,
                Prenom = prenom,
                Mail = mail,
                Telephone = telephone,
                PersonneMorale = personneMorale,
            });
            Context.SaveChanges();

            return IdClient;

        }

        /*------------------------------------------
        //Opérations sur les dossiers (Commercial ou client)
        -------------------------------------------*/
        public Guid CreateAssurance(bool annulation, decimal prix)
        {
            Guid assuranceId = Guid.NewGuid();
            Assurance ass = new Assurance {
                Id = assuranceId,
                Annulation = annulation,
                Prix = prix
            };
            Context.Assurances.Add(ass);
            Context.SaveChanges();
            return assuranceId;
        }
        public Guid CreateDossier(Guid voyageId, Guid clientId, Guid assuranceId, Guid commercialId, Etat etat = Etat.EnAttente)
        {
            Guid dossierId = Guid.NewGuid();
            Dossier doss = new Dossier {
                Id = dossierId,
                Voyage = voyageId,
                Client = clientId,
                Etat = (byte)etat,
                Assurance = assuranceId,
                Commercial = commercialId
            };
            Context.Dossiers.Add(doss);
            Context.SaveChanges();
            return dossierId;
        }
        public bool CreateDossierVoyageur(Guid dossierId, Guid voyageurId)
        {
            
            DossierVoyageur dossVoy = new DossierVoyageur
            {
                Dossier= dossierId,
                Voyageur = voyageurId,
            };
            Context.DossierVoyageurs.Add(dossVoy);
            Context.SaveChanges();
            return true;
        }
        public void DeleteDossier(Guid dossierId)
        {
            Dossier doss = Context.Dossiers.FirstOrDefault(d => d.Id == dossierId);
            Assurance ass = Context.Assurances.FirstOrDefault(a => a.Id == doss.Assurance);
            IQueryable<Voyageur> voyageurs = Context.DossierVoyageurs.Where(dv => dv.Dossier == dossierId).Select(dv => Context.Voyageurs.FirstOrDefault(v => v.Id == dv.Voyageur));
            
            // Suppression des voyageurs
            foreach(Voyageur voyageur in voyageurs)
            {
                Context.Voyageurs.Remove(voyageur);
            }

            // Suppression de l'assurance associée
            Context.Assurances.Remove(ass);

            // Suppression du dossier
            Context.Dossiers.Remove(doss);

            Context.SaveChanges();
        }
        public void ResetDossiers()
        {
            foreach(Dossier doss in Context.Dossiers)
            {
                DeleteDossier(doss.Id);
            }
            Context.SaveChanges();
        }
        public List<DossierDetailCommercial> GetDossiers()
        {
            return Context.Dossiers.Select(d => new DossierDetailCommercial {
                Id = d.Id,
                DateAller = d.Voyage1.DateAller,
                DateRetour = d.Voyage1.DateRetour,
                NbVoyageurs = (byte)Context.DossierVoyageurs.Where(dv => dv.Dossier == d.Id).Count(),
                Fournisseur = d.Voyage1.Fournisseur.ToString(),
                etat = (Etat) d.Etat
            }).ToList();
        }

        public void LoadDroits()
        {
            Dictionary<string, StatutEnum> etats = droits.Load();
            foreach (KeyValuePair<string, StatutEnum> kvp in etats)
            {
                if (!Roles.IsUserInRole(kvp.Key, kvp.Value.ToString())) Roles.AddUserToRole(kvp.Key, kvp.Value.ToString());

                if (Context.Employes.FirstOrDefault(e => e.Login == kvp.Key) is null)
                {
                    Context.Employes.Add(new Employe
                    {
                        Id = Guid.NewGuid(),
                        Login = kvp.Key,
                        MotDePasse = "Non traité", // Pas de stockage de mots de passe hors bdd locale
                        Statut = (byte)kvp.Value
                    });
                }
                else
                {
                    Context.Employes.FirstOrDefault(e => e.Login == kvp.Key).Statut = (byte)kvp.Value;
                }
            }
            Context.SaveChanges();
        }
        public Guid AddVoyageur(string nom, string prenom, DateTime naissance, bool isAccompagnant, string mail)
        {
            Guid IdVoyageur = Guid.NewGuid();
            Context.Voyageurs.Add(new Voyageur
            {
                Id = IdVoyageur,
                Nom = nom,
                Prenom = prenom,
                DateNaissance = naissance,
                IsAccompagnant = isAccompagnant,
                Mail = mail,
            });
            Context.SaveChanges();

            return IdVoyageur;
        }
    }
}
