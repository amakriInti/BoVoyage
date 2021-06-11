namespace BoVoyage.Scenario1.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dossier")]
    public partial class Dossier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dossier()
        {
            Voyageurs = new HashSet<Voyageur>();
        }

        public Guid Voyage { get; set; }

        public Guid Client { get; set; }

        public byte Etat { get; set; }

        public Guid? Assurance { get; set; }

        public Guid Id { get; set; }

        public Guid? Commercial { get; set; }

        public virtual Assurance Assurance1 { get; set; }

        public virtual Client Client1 { get; set; }

        public virtual Employe Employe { get; set; }

        public virtual Voyage Voyage1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Voyageur> Voyageurs { get; set; }
    }
}
