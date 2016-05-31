namespace BetSystem.Data.Common
{
    using System.Linq;

    using Models;

    public interface IDbRepository<T> : IDbRepository<T, int>
        where T : class, IAuditInfo, IDeletableEntity
    {
    }

    public interface IDbRepository<T, in TKey>
        where T : class, IAuditInfo, IDeletableEntity
    {
        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        T GetById(object id);

        void Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);

        void Save();
    }
}
