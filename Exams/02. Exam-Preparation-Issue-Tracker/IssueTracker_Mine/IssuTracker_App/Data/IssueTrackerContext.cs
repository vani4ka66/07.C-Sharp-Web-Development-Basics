using IssuTracker_App.Models;

namespace IssuTracker_App.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class IssueTrackerContext : DbContext
    {
        public IssueTrackerContext()
            : base("IssueTrackerContext")
        {
        }


        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Login> Logins { get; set; }

        public virtual DbSet<Issue> Issues { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}