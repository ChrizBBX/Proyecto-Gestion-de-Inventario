using Farsiman.Domain.Core.Standard.Repositories;
using System.Linq.Expressions;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Repository
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ProyectoGestionInventarioCaagContext _ProyectoGestionInventoryCaagContext;

        public EntityRepository(ProyectoGestionInventarioCaagContext context)
        {
            _ProyectoGestionInventoryCaagContext = context;
        }

        public void Add(TEntity entity)
        {
            _ProyectoGestionInventoryCaagContext.Set<TEntity>().Add(entity);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _ProyectoGestionInventoryCaagContext.Set<TEntity>().AsQueryable();
        }

        public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> query)
        {
            return _ProyectoGestionInventoryCaagContext.Set<TEntity>().FirstOrDefault();
        }

        public List<TEntity> Where(Expression<Func<TEntity, bool>> query)
        {
            return _ProyectoGestionInventoryCaagContext.Set<TEntity>().Where(query).ToList();
        }
    }
}
