using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage.Donnees
{
    public class DossierDetailCommercial
    {
        public Guid Id { get; set; }
        public System.DateTime DateAller { get; set; }
        public System.DateTime DateRetour { get; set; }
        public byte NbVoyageurs { get; set; }
        public string Fournisseur { get; set; }
        public Etat etat { get; set; }

    }
}
