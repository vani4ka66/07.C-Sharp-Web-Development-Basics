using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassDeffect.Data
{
    using MassDeffect.Data.Interfaces;
    using MassDeffect.Models;

    public class UnitOfWork : IUnitOfWork
    {
        private MassDeffectContext context;
        private IRepository<Anomaly> anomalies;
        private IRepository<Person> persons;
        private IRepository<Planet> planets;
        private IRepository<SolarSystem> solarSystems;
        private IRepository<Star> stars;

        public UnitOfWork()
        {
            this.context = new MassDeffectContext();
        }

        public IRepository<Anomaly> Anomalies => this.anomalies ?? (this.anomalies = new Repository<Anomaly>(this.context.Anomalies));

        public IRepository<Person> Persons => this.persons ?? (this.persons = new Repository<Person>(this.context.Persons));

        public IRepository<Planet> Planets => this.planets ?? (this.planets = new Repository<Planet>(this.context.Planets));

        public IRepository<SolarSystem> SolarSystems => this.solarSystems ?? (this.solarSystems = new Repository<SolarSystem>(this.context.SolarSystems));

        public IRepository<Star> Stars => this.stars ?? (this.stars = new Repository<Star>(this.context.Stars));

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
