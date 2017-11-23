namespace AcTester.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using AcTester.Models;

    public class AcTesterContext : DbContext
    {
        public AcTesterContext()
            : base("name=AcTesterContext")
        {
        }

        public DbSet<AirConditioner> AirConditioners { get; set; }

        public DbSet<Report> Reports { get; set; }
    }

}