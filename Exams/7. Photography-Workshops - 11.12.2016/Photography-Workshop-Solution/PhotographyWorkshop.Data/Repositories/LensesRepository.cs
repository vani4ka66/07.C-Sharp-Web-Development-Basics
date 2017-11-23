using PhotographyWorkshop.Models;
using System.Data.Entity;

namespace PhotographyWorkshop.Data.Repositories
{
    public class LensesRepository : Repository<Lens>
    {
        public LensesRepository(DbContext context) : base(context)
        {
        }
    }
}
