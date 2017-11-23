using PhotographyWorkshop.Models;
using System;

namespace PhotographyWorkshop.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Lens> Lenses { get; }
        IRepository<Accessory> Accesories { get; }
        IRepository<Camera> Cameras { get; }
        IRepository<Photographer> Photographers { get; }
        IRepository<Workshop> Workshops { get; }
        void Commit();
    }
}
