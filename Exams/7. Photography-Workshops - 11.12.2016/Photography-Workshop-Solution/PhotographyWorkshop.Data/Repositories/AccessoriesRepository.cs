using PhotographyWorkshop.Models;
using System.Data.Entity;

namespace PhotographyWorkshop.Data.Repositories
{
    public class AccessoriesRepository : Repository<Accessory>
    {
        public AccessoriesRepository(DbContext context) : base(context)
        {
        }
    }
}
