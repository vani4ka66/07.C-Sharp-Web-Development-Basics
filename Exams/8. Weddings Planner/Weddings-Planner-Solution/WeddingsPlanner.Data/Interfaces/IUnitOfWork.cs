using System;
using WeddingsPlanner.Models;

namespace WeddingsPlanner.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Agency> Agencies { get; }
        IRepository<Venue> Venues { get; }
        IRepository<Person> People { get; }
        IRepository<Wedding> Weddings { get; }
        IRepository<Invitation> Invitations { get; }
        IRepository<Present> Presents { get; }
        void Commit();
    }
}
