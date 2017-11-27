﻿namespace MassDeffect.Data.Interfaces
{
    using MassDeffect.Models;

    public interface IUnitOfWork
    {

        IRepository<Anomaly> Anomalies { get; }

        IRepository<Person> Persons { get; }

        IRepository<Planet> Planets { get; }

        IRepository<SolarSystem> SolarSystems { get; }

        IRepository<Star> Stars { get; }

        void Commit();
    }
}
