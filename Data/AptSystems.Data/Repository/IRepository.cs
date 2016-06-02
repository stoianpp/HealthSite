namespace AptSystems.Data.Repository
{
    using System;
    using System.Linq;

    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> All();

        T GetById(int id);

        T GetByGuidId(Guid id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        void DeleteByGuid(Guid id);

        void Detach(T entity);

        int SaveChanges();
    }
}