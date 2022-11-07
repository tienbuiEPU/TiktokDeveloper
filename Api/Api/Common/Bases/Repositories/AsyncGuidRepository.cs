using Api.Common.Bases.Persistence;
using Api.Common.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Common.Bases.Repositories
{
    public class AsyncGuidRepository<TEntity> : AsyncGenericRepository<TEntity, Guid>, IAsyncGuidRepository<TEntity>
        where TEntity : BaseEntity<Guid>
    {
        public AsyncGuidRepository(DbContext context) : base(context)
        {

        }
    }
}
