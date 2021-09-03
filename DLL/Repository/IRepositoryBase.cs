using DLL.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
   public interface IRepositoryBase<T> where T:class
    {
        IQueryable<T> QueryAll(Expression<Func<T, bool>> expression = null);
        Task <List<T>> GetList(Expression<Func<T, bool>> expression = null);
        Task CreateAsync(T Entry);
        Task CreateAsyncRange(List<T> EntryList);

        void Update(T Entry);
        void UpdateRange(List<T> EntryList);

        void Delete(T Entry);
        void DeleteRange(List<T> EntryList);

        Task<T> FindSingleAsync(Expression<Func<T, bool>> expression);

       



    }

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task CreateAsync(T Entry)
        {
             await _context.Set<T>().AddAsync(entity: Entry);
        }

        public async Task CreateAsyncRange(List<T> EntryList)
        {
            await _context.Set<T>().AddRangeAsync(EntryList);
        }

        public void Delete(T Entry)
        {
            _context.Set<T>().Remove(Entry);
        }

        public void DeleteRange(List<T> EntryList)
        {
            _context.Set<T>().RemoveRange(EntryList);
        }

        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetList(Expression<Func<T, bool>> expression = null)
        {
            return  expression != null ? await _context.Set<T>().Where(expression).AsNoTracking().ToListAsync<T>() : await _context.Set<T>().AsNoTracking().ToListAsync();

        }

        public IQueryable<T> QueryAll(Expression<Func<T, bool>> expression = null)
        {
            return expression != null ? _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().AsNoTracking();
        }

     

        public void Update(T Entry)
        {
            _context.Set<T>().Update(Entry);
        }

        public void UpdateRange(List<T> EntryList)
        {
            _context.Set<T>().UpdateRange(EntryList);
        }
    }
}
