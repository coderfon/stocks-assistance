using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.EF.Repositories
{
    public interface IDataRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> Get(int id);
        Task Add(TEntity entity, bool save = true);
        Task Update(TEntity entity, bool save = true);
        Task Delete(TEntity entity, bool save = true);
        Task SaveChanges();
    }
}
