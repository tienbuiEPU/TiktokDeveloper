using Api.Common.Bases;
using System;

namespace Api.Common.Interfaces.Repository
{
    public interface IAsyncGuidRepository<TEntity> : IAsyncGenericRepository<TEntity, Guid>
        where TEntity : BaseEntity<Guid>
    {
    }
}
