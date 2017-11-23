using System.Data.Entity;
using WeddingsPlanner.Models;

namespace WeddingsPlanner.Data.Repositories
{
    internal class PersonRepository : Repository<Person>
    {
        public PersonRepository(DbContext context) : base(context)
        {
        }
    }
}
