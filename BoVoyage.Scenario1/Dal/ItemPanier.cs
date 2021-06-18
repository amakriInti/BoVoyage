using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyage.Scenario1.Dal
{
    public class ItemPanier
    {
        public Voyage voyage { get; set; }

        public int nombre_voyageurs { get; set; }

        public int nombre_enfants { get; set; }

        public Guid id_dossier { get; set; }

        public Assurance assurance { get; set; }
    }
}