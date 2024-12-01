using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private Dictionary<Type, object> Repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

        }

        public GenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (this.Repositories == null)
            {
                this.Repositories = new Dictionary<Type, object>();
            }
            var type = typeof(TEntity);
            if (!this.Repositories.ContainsKey(type))
            {
                this.Repositories[type] = new GenericRepository<TEntity>(this._context);
            }

            return (GenericRepository<TEntity>)this.Repositories[type];
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
