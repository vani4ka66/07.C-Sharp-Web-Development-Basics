namespace WeddingsPlanner.Data
{
    using Models;
    using System.Data.Entity;

    public class WeddingPlannerContext : DbContext
    {
        public WeddingPlannerContext()
            : base("name=WeddingPlannerContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Present>()
                .HasKey(e => e.InvitationId);

            modelBuilder.Entity<Invitation>()
                .HasRequired(p => p.Present)
                .WithRequiredPrincipal(i => i.Invitation);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Venue> Venues { get; set; }
        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Wedding> Weddings { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Present> Presents { get; set; }
    }
}