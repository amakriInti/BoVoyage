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
    
    public partial class Voyage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voyage()
        {
            this.Dossiers = new HashSet<Dossier>();
            this.Destinations = new HashSet<Destination>();
        }
    
        public System.Guid Id { get; set; }
        public System.DateTime DateAller { get; set; }
        public System.DateTime DateRetour { get; set; }
        public byte MaxVoyageur { get; set; }
        public System.Guid Fournisseur { get; set; }
        public decimal PrixAchatTotal { get; set; }
        public decimal PrixVenteUnitaire { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dossier> Dossiers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Destination> Destinations { get; set; }
    }
}
