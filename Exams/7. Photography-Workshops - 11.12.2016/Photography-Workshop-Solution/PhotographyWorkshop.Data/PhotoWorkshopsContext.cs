namespace PhotographyWorkshop.Data
{
    using Models;
    using System.Data.Entity;

    public class PhotoWorkshopsContext : DbContext
    {
        public PhotoWorkshopsContext()
            : base("name=PhotoWorkshopsContext")
        {
        }

        public virtual DbSet<Camera> Cameras { get; set; }
        public virtual DbSet<Lens> Lenses { get; set; }
        public virtual DbSet<Accessory> Accessories { get; set; }
        public virtual DbSet<Photographer> Photographers { get; set; }
        public virtual DbSet<Workshop> Workshops { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photographer>()
                .HasRequired(photographer => photographer.PrimaryCamera)
                .WithMany(cameras => cameras.PrimaryCamerasPhotographers)
                .HasForeignKey(photographer => photographer.PrimaryCameraId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>()
                .HasRequired(p => p.SecondaryCamera)
                .WithMany(c => c.SecondaryCamerasPhotographers)
                .HasForeignKey(p => p.SecondaryCameraId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>()
                .HasMany(p => p.WorkshopsParticipated)
                .WithMany(w => w.Participants)
                .Map(entity =>
                    {
                        entity.ToTable("Enrollment");
                        entity.MapLeftKey("WorkshopId");
                        entity.MapRightKey("PhotographerId");
                    }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}