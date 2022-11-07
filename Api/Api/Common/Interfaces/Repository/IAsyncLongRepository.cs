using Api.Common.Bases;

namespace Api.Common.Interfaces.Repository
{
    public interface IAsyncLongRepository<TEntity> : IAsyncGenericRepository<TEntity, long>
        where TEntity : BaseEntity<long>
    {

    }
}
