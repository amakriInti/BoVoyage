namespace BoVoyage.Scenario1.Dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VoyageClient")]
    public partial class VoyageClient
    {
        [StringLength(10)]
        public string Id { get; set; }

        public Guid Voyage { get; set; }

        public Guid Client { get; set; }

        public byte Etat { get; set; }

        public Guid? Assurance { get; set; }

        public virtual Client Client1 { get; set; }

        public virtual Voyage Voyage1 { get; set; }
    }
}
