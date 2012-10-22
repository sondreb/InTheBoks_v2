namespace InTheBoks.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> where);

        T Get(Expression<Func<T, bool>> where);

        IEnumerable<T> GetAll();

        T GetById(long Id);

        T GetById(string Id);

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        IQueryable<T> Query();

        void Update(T entity);
    }
}