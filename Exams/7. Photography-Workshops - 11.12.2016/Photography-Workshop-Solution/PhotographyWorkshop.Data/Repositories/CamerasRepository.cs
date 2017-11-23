using PhotographyWorkshop.Models;
using System.Data.Entity;

namespace PhotographyWorkshop.Data.Repositories
{
    public class CamerasRepository : Repository<Camera>
    {
        public CamerasRepository(DbContext context) : base(context)
        {
        }
    }
}
