using System.Data.Entity;
using WeddingsPlanner.Models;

namespace WeddingsPlanner.Data.Repositories
{
    internal class VenueRepository : Repository<Venue>
    {
        public VenueRepository(DbContext context) : base(context)
        {
        }
    }
}
