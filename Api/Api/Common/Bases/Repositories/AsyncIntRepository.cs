using Api.Common.Bases.Persistence;
using Api.Common.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Common.Bases.Repositories
{
    public class AsyncIntRepository<TEntity> : AsyncGenericRepository<TEntity, int>, IAsyncIntRepository<TEntity>
        where TEntity : BaseEntity<int>
    {
        public AsyncIntRepository(DbContext context) : base(context)
        {

        }
    }
}
