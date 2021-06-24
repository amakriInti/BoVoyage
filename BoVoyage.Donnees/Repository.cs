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

        /*------------------------------------------
        //Récupérer les statuts des employés
        -------------------------------------------*/
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

        /*------------------------------------------
        //Remplissage combobox
        -------------------------------------------*/
        public object GetVoyageFormulaire(string continent, string pays, string region)
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
                             DescriptionVoyage = Voyage.Description,
                             DescriptionDestination = Destination.Description,
                             Continent = Destination.Continent,
                             Pays = Destination.Pays,
                             Region = Destination.Region
                         });
            //return query.ToList();
            if (query.Any(c => c.Region == region))
                return query.Where(b => b.Region == region).ToList();

            else if (query.Any(c => c.Pays == pays))
                return query.Where(c => c.Pays == pays).Select(c => c.Region).ToList().Distinct();

            else if (query.Any(c => c.Continent == continent))
                return query.Where(c => c.Continent == continent).Select(c => c.Pays).ToList().Distinct();

            else if (continent == null)
                return query.ToList();
            else
                return query;
        }

        /*------------------------------------------
        //Requetes divers Voyage
        -------------------------------------------*/
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

            if (tri == "Fournisseur")
                if (choix == "null" || choix == null) return query.Select(c => c.Fournisseur).ToList().Distinct();
                else return query.Where(c => c.Fournisseur == choix).ToList();
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

            /*---------------------
            //Ancienne version
            ---------------------*/
            //return query.ToList();
            /*if (tri == "DateAller")
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
                return query;*/
        }

        /*------------------------------------------
        //Requete de tout les éléments d'un voyage
        -------------------------------------------*/
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

        /*------------------------------------------
        //Remplissage combobox (à remodifier) 
        -------------------------------------------*/
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

        /*---------------------------------------------Travail de vincent ------------------------------------*/
        /*------------------------------------------
        //Création du client avant insertion dans DB
        -------------------------------------------
        public Client CreateClient(string nom, string mail, string telephone, string prenom, string personneMorale)
        {
            return new Client
            {
                Id = Guid.NewGuid(),
                Nom = nom,
                Prenom = prenom,
                Mail = mail,
                Telephone = telephone,
                PersonneMorale = personneMorale,
            };
        }*/

        /*------------------------------------------
        //Ajout du client dans DB
        -------------------------------------------
        public bool AddClient(Guid Id, string Nom, string Prenom, string Mail, string PersonneMorale, string Telephone)
        {
            Context.Clients.Add(new Client
            {
                Id = Id,
                Nom = Nom,
                Prenom = Prenom,
                Mail = Mail,
                PersonneMorale = PersonneMorale,
                Telephone = Telephone,
            });
            Context.SaveChanges();
            return true;
        }*/

        /*------------------------------------------
        //Création du voyageur avant insertion dans DB
        -------------------------------------------
        public Voyageur CreateVoyageur(string nom, string prenom, DateTime naissance, bool isAccompagnant, string mail)
        {

            return new Voyageur
            {
                Id = Guid.NewGuid(),
                Nom = nom,
                Prenom = prenom,
                DateNaissance = naissance,
                IsAccompagnant = isAccompagnant,
                Mail = mail,
            };
        }*/

        /*------------------------------------------
        //Ajout des voyageurs dans DB
        -------------------------------------------
        public bool AddVoyageurs(Guid Id, string Nom, string Prenom, string Mail, DateTime DateNaissance, bool IsAccompagnant)
        {
            Context.Voyageurs.Add(new Voyageur
            {
                Id = Id,
                Nom = Nom,
                Prenom = Prenom,
                Mail = Mail,
                DateNaissance = DateNaissance,
                IsAccompagnant = IsAccompagnant,
            });
            Context.SaveChanges();
            return true;
        }*/

        /*------------------------------------------
        //Création de l'assurrance avant insertion dans DB (a modifier)
        -------------------------------------------
        public Assurance CreateAssurance(bool annulation, decimal? prix)
        {
            return new Assurance
            {
                Id = Guid.NewGuid(),
                Annulation = annulation,
                Prix = prix
            };
        }*/

        /*------------------------------------------
        //Ajout des assurrances dans DB (a modifier)
        -------------------------------------------
        public bool AddAssurance(Guid Id, bool Annulation, decimal? Prix)
        {
            Context.Assurances.Add(new Assurance
            {
                Id = Id,
                Annulation = Annulation,
                Prix = Prix,
            });
            Context.SaveChanges();
            return true;
        }*/

        /*------------------------------------------
        //Création du dossier avant insertion dans DB
        -------------------------------------------
        public Dossier CreateDossier(Guid voyageId, Guid clientId, Guid? assuranceId)
        {
            return new Dossier
            {
                Id = Guid.NewGuid(),
                Voyage = voyageId,
                Client = clientId,
                Etat = (byte)Etat.EnAttente,
                Assurance = assuranceId,
                Commercial = null
            };
        }*/

        /*------------------------------------------
        //Ajout des dossiers dans DB
        -------------------------------------------
        public bool AddDossier(Guid Id, Guid Voyage, Guid Client, Guid? Assurance, Guid? Commercial, byte Etat)
        {
            Context.Dossiers.Add(new Dossier
            {
                Id = Id,
                Voyage = Voyage,
                Client = Client,
                Assurance = Assurance,
                Commercial = Commercial,
                Etat = Etat,
            });
            Context.SaveChanges();
            return true;
        }*/

        /*------------------------------------------
        //Ajout des dossiers voyageur dans DB
        -------------------------------------------
        public bool AddDossierVoyageurs(Guid dossierId, Guid voyageurId)
        {
            Guid dossiervoyageurId = Guid.NewGuid();
            DossierVoyageur dossVoy = new DossierVoyageur
            {
                Id = dossiervoyageurId,
                Dossier = dossierId,
                Voyageur = voyageurId,
            };
            Context.DossierVoyageurs.Add(dossVoy);
            Context.SaveChanges();
            return true;
        }*/

        /*---------------------------------------------New version ------------------------------------*/
        /*------------------------------------------
        //Ajout du client dans DB
        -------------------------------------------*/
        public bool AddClient(Guid Id, string PersonneMorale, string Nom, string Prenom, string Mail, string Telephone)
        {
            Context.Clients.Add(new Client
            {
                Id = Id,
                Nom = Nom,
                Prenom = Prenom,
                Mail = Mail,
                PersonneMorale = PersonneMorale,
                Telephone = Telephone,
            });
            Context.SaveChanges();
            return true;
        }

        /*------------------------------------------
        //Ajout des voyageurs dans DB
        -------------------------------------------*/
        public bool AddVoyageurs(Guid Id, string Nom, string Prenom, DateTime DateNaissance,bool IsAccompagnant, string Mail)
        {
            Context.Voyageurs.Add(new Voyageur
            {
                Id = Id,
                Nom = Nom,
                Prenom = Prenom,
                Mail = Mail,
                DateNaissance = DateNaissance,
                IsAccompagnant = IsAccompagnant,
            });
            Context.SaveChanges();
            return true;
        }

        /*------------------------------------------
        //Ajout des assurrances dans DB (a modifier)
        -------------------------------------------*/
        public bool AddAssurance(Guid Id, bool Annulation, decimal? Prix)
        {
            Context.Assurances.Add(new Assurance
            {
                Id = Id,
                Annulation = Annulation,
                Prix = Prix
            }) ;

            Context.SaveChanges();
            return true;
        }

        /*------------------------------------------
        //Ajout des dossiers dans DB
        -------------------------------------------*/
        public bool AddDossier(Guid IdDossier, Guid IdVoyage, Guid IdClient, byte Etat, Guid IdAssurance, Guid? IdCommercial)
        {
            Context.Dossiers.Add(new Dossier
            {
                Id = IdDossier,
                Voyage = IdVoyage,
                Client = IdClient,
                Etat = Etat,
                Assurance = IdAssurance,
                Commercial = IdCommercial
            });
            Context.SaveChanges();
            return true;
        }

        /*------------------------------------------
        //Ajout des dossiers voyageur dans DB
        -------------------------------------------*/
        public bool AddDossierVoyageurs(Guid dossierId, Guid voyageurId)
        {
            Guid dossiervoyageurId = Guid.NewGuid();
            Context.DossierVoyageurs.Add(new DossierVoyageur
            {
                Id = dossiervoyageurId,
                Dossier = dossierId,
                Voyageur = voyageurId,
            });
            Context.SaveChanges();
            return true;
        }

        /*------------------------------------------
        //Suppression d'un dossier (à vérifier)
        -------------------------------------------*/
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

        /*------------------------------------------
        //Suppression de tout les dossiers (à vérifier)
        -------------------------------------------*/
        public void ResetDossiers()
        {
            foreach(Dossier doss in Context.Dossiers)
            {
                DeleteDossier(doss.Id);
            }
            Context.SaveChanges();
        }


        /*------------------------------------------
        //Affichage statut employé (à vérififer)
        -------------------------------------------*/
        public IQueryable<Employe> GetCommerciaux()
        {
            return Context.Employes.Where(e => e.Statut == (byte)StatutEnum.Commercial);
        }

        /*------------------------------------------
        //List de tout les dossier pour les commerciaux (à vérifier)
        -------------------------------------------*/
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

        /*------------------------------------------
        //Chargement des doits
        -------------------------------------------*/
        public void LoadDroits()
        {
            //Récupère les autorisations de la classe données>droits et les stocke dans un doctionnaire
            Dictionary<string, StatutEnum> etats = droits.Load();

            //Ajoute les droits défini dans la base dedonnée
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
    }
}
