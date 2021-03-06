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
    
    public partial class Dossier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dossier()
        {
            this.Voyageurs = new HashSet<Voyageur>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid Voyage { get; set; }
        public System.Guid Client { get; set; }
        public byte Etat { get; set; }
        public Nullable<System.Guid> Assurance { get; set; }
        public Nullable<System.Guid> Commercial { get; set; }
    
        public virtual Assurance Assurance1 { get; set; }
        public virtual Client Client1 { get; set; }
        public virtual Employe Employe { get; set; }
        public virtual Voyage Voyage1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Voyageur> Voyageurs { get; set; }
    }
}
