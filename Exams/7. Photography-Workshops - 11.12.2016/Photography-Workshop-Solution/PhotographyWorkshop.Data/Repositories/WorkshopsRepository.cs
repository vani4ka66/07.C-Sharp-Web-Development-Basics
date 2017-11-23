using PhotographyWorkshop.Models;
using System.Data.Entity;

namespace PhotographyWorkshop.Data.Repositories
{
    public class WorkshopsRepository : Repository<Workshop>
    {
        public WorkshopsRepository(DbContext context) : base(context)
        {
        }
    }
}
