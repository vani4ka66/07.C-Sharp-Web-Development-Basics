using System.Data.Entity;
using WeddingsPlanner.Models;

namespace WeddingsPlanner.Data.Repositories
{
    internal class WeddingRepository : Repository<Wedding>
    {
        public WeddingRepository(DbContext context) : base(context)
        {
        }
    }
}
