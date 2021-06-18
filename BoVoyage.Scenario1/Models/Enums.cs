using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyage.Scenario1.Models
{
    public enum StatutEnum
    {
        Inconnu = 0, Commercial = 1, Admin = 2
    }

    public enum DossierEnum // Enum pour le statut des dossiers
    {
        EnAttente = 0, EnCours = 1, Refuse = 2, Accepte = 3
    }
}