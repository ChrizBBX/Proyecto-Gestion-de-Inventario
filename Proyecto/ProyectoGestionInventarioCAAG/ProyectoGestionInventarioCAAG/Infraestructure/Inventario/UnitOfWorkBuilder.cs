using Farsiman.Domain.Core.Standard.Repositories;
using Farsiman.Infraestructure.Core.Entity.Standard;
using Microsoft.EntityFrameworkCore;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario
{
    public class UnitOfWorkBuilder
    {
        readonly IServiceProvider _serviceProvider;

        public UnitOfWorkBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork BuilderProyectoGestionInventarioCAAG()
        {
            DbContext dbContext = _serviceProvider.GetService<ProyectoGestionInventarioCaagContext>() ?? throw new NullReferenceException();
            return new UnitOfWork(dbContext);
        }
    }
}
