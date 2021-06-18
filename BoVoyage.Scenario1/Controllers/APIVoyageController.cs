using BoVoyage.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BoVoyage.Scenario1.Controllers
{
    
    public class APIVoyageController : ApiController
    {
        public Guid IdVoyageur;
        private ClassMetier metier = new ClassMetier();
        public bool PostParticipant(Participant obj)
        {
            string nom = obj.nom;
            string prenom = obj.prenom;
            string mail = obj.mail;
            DateTime naissance = obj.naissance;
            bool isAccompagnant = obj.isAccompagnant;
            IdVoyageur = metier.AddVoyageurs(nom, prenom, naissance, isAccompagnant, mail);
            //metier.CreateDossierVoyageur(, IdVoyageur);
            return true;
        }
        
    }
    public class Participant
    {
        public string nom { get; set; }
        public string prenom { get; set; }
        public string mail { get; set; }
        public bool isAccompagnant { get; set; }
        public DateTime naissance { get; set; }
    }
      
}
