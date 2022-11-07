using Api.Common.Bases;

namespace Api.Common.Interfaces.Repository
{
    public interface IAsyncIntRepository<TEntity> : IAsyncGenericRepository<TEntity, int>
        where TEntity : BaseEntity<int>
    {

    }
}
