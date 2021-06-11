namespace BoVoyage.Scenario1.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Voyageur")]
    public partial class Voyageur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voyageur()
        {
            Dossiers = new HashSet<Dossier>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateNaissance { get; set; }

        public bool IsAccompagnant { get; set; }

        public string Mail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dossier> Dossiers { get; set; }
    }
}
