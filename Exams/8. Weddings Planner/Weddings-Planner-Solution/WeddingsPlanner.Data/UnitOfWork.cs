namespace WeddingsPlanner.Data
{
    using Interfaces;
    using Models;
    using Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly WeddingPlannerContext context;

        public UnitOfWork(WeddingPlannerContext context)
        {
            this.context = context;
            this.Agencies = new AgencyRepository(context);
            this.Invitations = new InvitationRepository(context);
            this.People = new PersonRepository(context);
            this.Venues = new VenueRepository(context);
            this.Presents = new PresentRepository(context);
            this.Weddings = new WeddingRepository(context);
        }

        public IRepository<Agency> Agencies { get; private set; }

        public IRepository<Invitation> Invitations { get; private set; }

        public IRepository<Person> People { get; private set; }

        public IRepository<Present> Presents { get; private set; }

        public IRepository<Venue> Venues { get; private set; }

        public IRepository<Wedding> Weddings { get; private set; }

        public void Commit()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
