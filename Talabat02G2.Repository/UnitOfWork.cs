using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TalabatG02.Core;
using TalabatG02.Core.Entities;
using TalabatG02.Core.Entities.OrderAggregtion;
using TalabatG02.Core.Repositories;
using TalabatG02.Repository.Data;

namespace TalabatG02.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext context;
        private Hashtable _repositorys;

        public UnitOfWork(StoreContext context)
        {
            this.context = context;
            _repositorys = new Hashtable();
        }
        IGenericRepository<TEntity> IUnitOfWork.Repository<TEntity>()
        {
            var type = typeof(TEntity).Name;

            if (!_repositorys.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(context);
                _repositorys.Add(type, repository);
            }
            return _repositorys[type]as IGenericRepository<TEntity>;
        }


        public async Task<int> Complete()
            => await context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await context.DisposeAsync();

   
    }
}
