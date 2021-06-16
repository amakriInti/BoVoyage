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


        //--------------------------------------------AJOUT VOYAGE AU PANIER ---------------------------------------------------

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

        //--------------------------------------------SUPPRIMER VOYAGE DU PANIER ---------------------------------------------------


        public void Supprimer(Guid LigneID)
        {
            LignePanier ligne = _context.LignePaniers.SingleOrDefault(s => s.Id == LigneID);

            if ( ligne != null)
            {
                _context.LignePaniers.Remove(ligne);
                _context.SaveChanges();
            }
        }

        //--------------------------------------------EXTRACTION D'UNE LIGNE DU PANIER---------------------------------------------------

        public LignePanier Ligne(Guid LigneID)
        {
            return _context.LignePaniers.Include(l => l.Voyage).FirstOrDefault(m => m.Id == LigneID);
        }

        //--------------------------------------------EXTRACTION DE TOUTES LES LIGNES DU PANIER ---------------------------------------------------

        public IList<LignePanier> Ligne()
        {
            IList<LignePanier> ls = _context.LignePaniers.Where(s => s.PanierID == _panierID).Include(l => l.Voyage).ToList();
            return ls;
        }

        //--------------------------------------------TOTAL DU PANIER ---------------------------------------------------

        //public decimal Total()
        //{
        //    decimal T = _context.LignePaniers.Sum(s => s.Montant());
        //    return T;
        //}

        //--------------------------------------------NOMBRE DE LIGNES DU PANIER ---------------------------------------------------

        public int Nombre()
        {
            int n = _context.LignePaniers.Where(s => s.PanierID == _panierID).ToList().Count;
            return n;
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

    }
}