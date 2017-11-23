namespace DossierSystem
{
    using System.Data.Entity;
    using Models;

    public class DossierContext : DbContext
    {
        public DossierContext()
            : base("DossierContext")
        {
        }

        public virtual IDbSet<Activity> Activities { get; set; }

        public virtual IDbSet<Individual> Individuals { get; set; }

        public virtual IDbSet<Location> Locations { get; set; }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<ActivityType> ActivityTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Individual>()
                .HasMany(i => i.Related)
                .WithMany()
                .Map(mapper =>
                {
                    mapper.ToTable("RelatedIndividuals");
                    mapper.MapLeftKey("IndividualId");
                    mapper.MapRightKey("RelatedId");
                });
        }
    }
}