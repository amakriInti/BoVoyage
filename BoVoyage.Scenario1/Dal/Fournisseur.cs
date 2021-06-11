namespace BoVoyage.Scenario1.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Fournisseur")]
    public partial class Fournisseur
    {
        public Guid Id { get; set; }

        [Required]
        public string Nom { get; set; }
    }
}
