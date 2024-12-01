using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        Task SaveChangesAsync();
        void SaveChanges();
    }
}
