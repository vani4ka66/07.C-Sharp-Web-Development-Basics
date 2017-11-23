namespace MassDeffect.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using MassDeffect.Models;

    public class MassDeffectContext : DbContext
    {
        public MassDeffectContext()
            : base("name=MassDeffectContext")
        {
        }

        public DbSet<Anomaly> Anomalies { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Planet> Planets { get; set; }

        public DbSet<SolarSystem> SolarSystems { get; set; }

        public DbSet<Star> Stars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anomaly>()
                .HasOptional(anom => anom.OriginPlanet)
                .WithMany(planet => planet.OriginOfAnomalies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
                .HasOptional(anom => anom.TeleportPlanet)
                .WithMany(planet => planet.TeleportOfAnomalies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
                .HasMany(anom => anom.Victims)
                .WithMany(person => person.Anomalies)
                .Map(configuration =>
                {
                    configuration.MapLeftKey("AnomalyId");
                    configuration.MapRightKey("PersonId");
                    configuration.ToTable("AnomalyVictims");
                });

            base.OnModelCreating(modelBuilder);
        }
    }


}