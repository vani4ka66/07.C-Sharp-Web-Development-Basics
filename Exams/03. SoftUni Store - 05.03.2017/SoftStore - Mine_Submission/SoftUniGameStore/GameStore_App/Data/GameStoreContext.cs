using GameStore_App.Models;

namespace GameStore_App.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class GameStoreContext : DbContext
    {
      
        public GameStoreContext()
            : base("GameStoreContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Game> Games { get; set; }

        public virtual DbSet<Login> Logins { get; set; }


    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}