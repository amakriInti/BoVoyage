﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BoVoyageContext : DbContext
    {
        public BoVoyageContext()
            : base("name=BoVoyageContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Assurance> Assurances { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<Dossier> Dossiers { get; set; }
        public virtual DbSet<Employe> Employes { get; set; }
        public virtual DbSet<Voyage> Voyages { get; set; }
        public virtual DbSet<Voyageur> Voyageurs { get; set; }
    }
}
