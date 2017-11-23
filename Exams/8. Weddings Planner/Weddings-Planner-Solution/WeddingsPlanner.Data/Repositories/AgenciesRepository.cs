using System.Data.Entity;
using WeddingsPlanner.Models;

namespace WeddingsPlanner.Data.Repositories
{
    internal class AgencyRepository : Repository<Agency>
    {
        public AgencyRepository(DbContext context) : base(context)
        {
        }
    }
}
