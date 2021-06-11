namespace BoVoyage.Scenario1.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Voyage")]
    public partial class Voyage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voyage()
        {
            Dossiers = new HashSet<Dossier>();
            VoyageClients = new HashSet<VoyageClient>();
            Destinations = new HashSet<Destination>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Libelle { get; set; }

        public DateTime DateAller { get; set; }

        public DateTime DateRetour { get; set; }

        public byte MaxVoyageurs { get; set; }

        [Required]
        public string Fournisseur { get; set; }

        public decimal PrixAchatTotal { get; set; }

        public decimal PrixVenteUnitaire { get; set; }

        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dossier> Dossiers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoyageClient> VoyageClients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Destination> Destinations { get; set; }
    }
}
