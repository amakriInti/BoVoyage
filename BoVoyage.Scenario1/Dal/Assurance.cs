//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BoVoyage.Scenario1.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class Assurance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Assurance()
        {
            this.Dossiers = new HashSet<Dossier>();
        }
    
        public System.Guid Id { get; set; }
        public bool Annulation { get; set; }
        public Nullable<decimal> Prix { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dossier> Dossiers { get; set; }
    }
}