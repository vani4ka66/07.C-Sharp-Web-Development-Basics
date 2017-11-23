using _01.MassDefect_CodeFirst.Models;

namespace _01.MassDefect_CodeFirst
{
    using System.Data.Entity;

    public class MassDefectContext : DbContext
    {
        public MassDefectContext()
            : base("name=MassDefectContext")
        {
            Database.SetInitializer<MassDefectContext>(new CreateDatabaseIfNotExists<MassDefectContext>());
        }

        public virtual IDbSet<SolarSystem> SolarSystems { get; set; }

        public virtual IDbSet<Star> Stars { get; set; }

        public virtual IDbSet<Planet> Planets { get; set; }

        public virtual IDbSet<Person> Persons { get; set; }

        public virtual IDbSet<Anomaly> Anomalies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Anomaly>()
                .HasMany(anomaly => anomaly.Victims)
                .WithMany(person => person.Anomalies)
                .Map(entity =>
                    {
                        entity.ToTable("AnomalyVictims");
                        entity.MapLeftKey("AnomalyId");
                        entity.MapRightKey("PersonId");
                    }
                );
        }
    }
}