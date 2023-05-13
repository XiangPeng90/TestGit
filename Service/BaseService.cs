using System.Linq.Expressions;
using System.Net.Http;
using Entities;
using IService;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class BaseService:IBaseService
    {
        private readonly Context _context;

        public BaseService(Context context)
        {
            _context = context;
        }

        public IQueryable<T> Login<T>(Expression<Func<T, bool>> where) where T : class
        {
            return where is null ? (IQueryable<T>)_context.Set<T>() : _context.Set<T>().Where(where);
        }

        public async Task<T?> Regist<T>(T obj, Expression<Func<T, bool>> where) where T : class
        {
            T? o = await _context.Set<T>().Where(where).SingleOrDefaultAsync();
            if (o is null)
            {
                await _context.AddAsync(obj);
                await _context.SaveChangesAsync();
                return null;
            }
            else
                return o;
        }
    }
}

