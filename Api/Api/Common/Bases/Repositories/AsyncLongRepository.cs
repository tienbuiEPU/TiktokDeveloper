
using Api.Common.Bases.Persistence;
using Api.Common.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Common.Bases.Repositories
{
    public class AsyncLongRepository<TEntity> : AsyncGenericRepository<TEntity, long>, IAsyncLongRepository<TEntity>
        where TEntity : BaseEntity<long>
    {
        public AsyncLongRepository(DbContext context) : base(context)
        {

        }
    }
}
