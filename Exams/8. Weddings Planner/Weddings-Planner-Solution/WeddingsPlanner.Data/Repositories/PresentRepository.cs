using System.Data.Entity;
using WeddingsPlanner.Models;

namespace WeddingsPlanner.Data.Repositories
{
    internal class PresentRepository : Repository<Present>
    {
        public PresentRepository(DbContext context) : base(context)
        {
        }
    }
}
