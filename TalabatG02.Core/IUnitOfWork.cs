using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG02.Core.Entities;
using TalabatG02.Core.Entities.OrderAggregtion;
using TalabatG02.Core.Repositories;

namespace TalabatG02.Core
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity:BaseEntity;  

        Task<int> Complete();

    }
}
