namespace MountainsCodeFirst
{
    using System.Data.Entity;

    using MountainsCodeFirst.Migrations;

    public class MountainsContext : DbContext
    {
        public MountainsContext()
            : base("name=MountainsContext")
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Mountain> Mountains { get; set; }
        public virtual DbSet<Peak> Peaks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MountainsMigration());
        }
    }
}
