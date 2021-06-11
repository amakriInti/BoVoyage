using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BoVoyage.Scenario1.Dal
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public virtual DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public virtual DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public virtual DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public virtual DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public virtual DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public virtual DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public virtual DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public virtual DbSet<aspnet_Users> aspnet_Users { get; set; }
        public virtual DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public virtual DbSet<Assurance> Assurances { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<Dossier> Dossiers { get; set; }
        public virtual DbSet<Employe> Employes { get; set; }
        public virtual DbSet<Fournisseur> Fournisseurs { get; set; }
        public virtual DbSet<Voyage> Voyages { get; set; }
        public virtual DbSet<VoyageClient> VoyageClients { get; set; }
        public virtual DbSet<Voyageur> Voyageurs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Paths)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Roles)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Users)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Paths>()
                .HasOptional(e => e.aspnet_PersonalizationAllUsers)
                .WithRequired(e => e.aspnet_Paths);

            modelBuilder.Entity<aspnet_Roles>()
                .HasMany(e => e.aspnet_Users)
                .WithMany(e => e.aspnet_Roles)
                .Map(m => m.ToTable("aspnet_UsersInRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Profile)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventSequence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventOccurrence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<Assurance>()
                .HasMany(e => e.Dossiers)
                .WithOptional(e => e.Assurance1)
                .HasForeignKey(e => e.Assurance);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Dossiers)
                .WithRequired(e => e.Client1)
                .HasForeignKey(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.VoyageClients)
                .WithRequired(e => e.Client1)
                .HasForeignKey(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Destination>()
                .HasMany(e => e.Voyages)
                .WithMany(e => e.Destinations)
                .Map(m => m.ToTable("DestinationVoyage").MapLeftKey("Destination").MapRightKey("Voyage"));

            modelBuilder.Entity<Dossier>()
                .HasMany(e => e.Voyageurs)
                .WithMany(e => e.Dossiers)
                .Map(m => m.ToTable("DossierVoyageur").MapLeftKey("Dossier").MapRightKey("Voyageur"));

            modelBuilder.Entity<Employe>()
                .HasMany(e => e.Dossiers)
                .WithOptional(e => e.Employe)
                .HasForeignKey(e => e.Commercial);

            modelBuilder.Entity<Voyage>()
                .HasMany(e => e.Dossiers)
                .WithRequired(e => e.Voyage1)
                .HasForeignKey(e => e.Voyage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Voyage>()
                .HasMany(e => e.VoyageClients)
                .WithRequired(e => e.Voyage1)
                .HasForeignKey(e => e.Voyage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VoyageClient>()
                .Property(e => e.Id)
                .IsFixedLength();
        }
    }
}
