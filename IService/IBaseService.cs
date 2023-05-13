using System.Linq.Expressions;

namespace IService
{
    public interface IBaseService
    {
        public IQueryable<T> Login<T>(Expression<Func<T, bool>> where) where T : class;

        public Task<T?> Regist<T>(T obj, Expression<Func<T, bool>> where) where T : class;

    }
}

