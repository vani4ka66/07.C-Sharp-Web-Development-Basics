using System.Data.Entity;
using WeddingsPlanner.Models;

namespace WeddingsPlanner.Data.Repositories
{
    internal class InvitationRepository : Repository<Invitation>
    {
        public InvitationRepository(DbContext context) : base(context)
        {
        }
    }
}
