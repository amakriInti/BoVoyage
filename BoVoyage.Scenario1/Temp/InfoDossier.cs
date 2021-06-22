using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyage.Scenario1.Temp
{
    public class InfoDossier2
    {
        public Guid[] IdVoyageurs { get; set; }

        public void AjoutVoyageur(Guid idVoyageur)
        {
            IdVoyageurs.Append(idVoyageur);
        }
        public void ReinitVoyageur()
        {
            IdVoyageurs = null;
        }
    }
}