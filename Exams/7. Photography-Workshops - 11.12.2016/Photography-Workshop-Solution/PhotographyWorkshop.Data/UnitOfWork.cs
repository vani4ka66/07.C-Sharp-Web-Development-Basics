using PhotographyWorkshop.Data.Interfaces;
using PhotographyWorkshop.Data.Repositories;
using PhotographyWorkshop.Models;

namespace PhotographyWorkshop.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PhotoWorkshopsContext context;

        public UnitOfWork(PhotoWorkshopsContext context)
        {
            this.context = context;
            this.Accesories = new AccessoriesRepository(context);
            this.Cameras = new CamerasRepository(context);
            this.Lenses = new LensesRepository(context);
            this.Photographers = new PhotographersRepository(context);
            this.Workshops = new WorkshopsRepository(context);
        }

        public IRepository<Accessory> Accesories { get; private set; }

        public IRepository<Camera> Cameras { get; private set; }

        public IRepository<Lens> Lenses { get; private set; }

        public IRepository<Photographer> Photographers { get; private set; }

        public IRepository<Workshop> Workshops { get; private set; }

        public void Commit()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
