using BoVoyage.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyage.Scenario1.Models
{
    /*----------------------------------
    //Classe enregistrement des données temp du panier
    -----------------------------------*/
    public class Panier
    {
        private ClassMetier metier = new ClassMetier();

        //Table Dossier
        public Guid IdDossier { get; set; }
        public Guid IdVoyage { get; set; }
        //public Guid IdClient { get; set; }
        public byte Etat { get; set; }
        //public Guid IdAssurrance { get; set; }
        public Guid? IdCommercial { get; set; }

        //Table Client
        public Guid IdClient { get; set; }
        public string PersonneMorale { get; set; }
        public string NomClient { get; set; }
        public string PrenomClient { get; set; }
        public string MailClient { get; set; }
        public string Telephone { get; set; }

        //Table Voyageur
        public List<VoyageursPanier> VoyageurPanier { get; set; }

        //TableAssurance
        public Guid IdAssurance { get; set; }
        public bool Annulation { get; set; }
        public decimal? Prix { get; set; }

        //TableDossierVoyageur
        public Guid IdDossierVoyageur { get; set; }
        //public Guid IdVoyageur { get; set; }
        //public Guid IdDossier { get; set; }

    }

    //Detail table Voyageur
    public class VoyageursPanier
    {
        public Guid IdVoyageur { get; set; }
        public string NomVoyageur { get; set; }
        public string PrenomVoyageur { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public bool Accompagnant { get; set; }
        public string MailVoyageur { get; set; }
    }
}