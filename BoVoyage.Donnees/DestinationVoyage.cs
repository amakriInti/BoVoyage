//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BoVoyage.Donnees
{
    using System;
    using System.Collections.Generic;
    
    public partial class DestinationVoyage
    {
        public System.Guid Destination { get; set; }
        public System.Guid Voyage { get; set; }
        public long id { get; set; }
        public string DestinationVoyage1 { get; set; }
    
        public virtual Destination Destination1 { get; set; }
        public virtual Voyage Voyage1 { get; set; }
    }
}
