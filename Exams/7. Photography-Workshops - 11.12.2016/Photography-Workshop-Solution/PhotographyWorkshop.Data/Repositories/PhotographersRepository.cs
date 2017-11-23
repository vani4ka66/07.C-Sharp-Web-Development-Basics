using PhotographyWorkshop.Models;
using System.Data.Entity;

namespace PhotographyWorkshop.Data.Repositories
{
    public class PhotographersRepository : Repository<Photographer>
    {
        public PhotographersRepository(DbContext context) : base(context)
        {
        }
    }
}
