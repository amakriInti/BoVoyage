using BoVoyage.Scenario1.Dal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BoVoyage.Scenario1.Models
{
    public class Panier : IDisposable
    {
        private BoVoyageContext _context;
        private string _panierID;
        

        public Panier( BoVoyageContext context, string PanierID)
        {
            _context = context;
            _panierID = PanierID;
        }


        //--------------------------------------------AJOUT VOYAGE ---------------------------------------------------

        public void Ajouter(Guid Voyage_Id)
        {
            LignePanier Ligne = _context.LignePaniers.SingleOrDefault(s => s.PanierID == _panierID && s.VoyageID == Voyage_Id);
            if (Ligne == null)
            {
                Ligne = new LignePanier
                {
                    PanierID = _panierID,
                    VoyageID = Voyage_Id,
                    NombreVoyageur = 1,
                };
                _context.LignePaniers.Add(Ligne);
            }
            else
                Ligne.NombreVoyageur++;

            _context.SaveChanges();
        }

        public void Supprimer(Guid LigneID)
        {
            LignePanier ligne = _context.LignePaniers.SingleOrDefault(s => s.Id == LigneID);

            if ( ligne != null)
            {
                _context.LignePaniers.Remove(ligne);
                _context.SaveChanges();
            }
        }

        public decimal Total()
        {
            decimal T = _context.LignePaniers.Where(s => s.PanierID == _panierID).Include(l => l.Voyage).Sum(s => (s.Montant()));
            return T;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}