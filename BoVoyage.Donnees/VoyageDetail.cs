using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage.Donnees
{
    public class VoyageDetail
    {
            public System.Guid Id { get; set; }
            public System.DateTime DateAller { get; set; }
            public System.DateTime DateRetour { get; set; }
            public byte MaxVoyageur { get; set; }
            public string Fournisseur { get; set; }
            public decimal PrixAchatTotal { get; set; }
            public decimal PrixVenteUnitaire { get; set; }
            public string DescriptionVoyage { get; set; }
            public string DescriptionDestination { get; set; }
            public string Continent { get; set; }
            public string Pays { get; set; }
            public string Region { get; set; }
            public string Image { get; set; }
        
    }
}
