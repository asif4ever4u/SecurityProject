using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Objects;
using System.Data;
using System.Data.Objects.DataClasses;

namespace SecurityEF.Repositories
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        /// <summary>
        /// Saves changes on given context against Database
        /// </summary>
        void SaveChanges();
        /// <summary>
        /// Saves changes on given context against Database
        /// </summary>
        /// <param name="option"></param>
        void SaveChanges(SaveOptions option);

        /// <summary>
        /// Adds entity to the context
        /// </summary>
        /// <param name="entity">Entity</param>
        void Add(TEntity entity);
        /// <summary>
        /// Adds entities to the context
        /// </summary>
        /// <param name="entity">Entities</param>
        void Add(IEnumerable<TEntity> entities);
        /// <summary>
        /// Edits entity on the context
        /// </summary>
        /// <param name="entity">Entity</param>
        void Edit(TEntity entity);
        /// <summary>
        /// Edits entities on the context
        /// </summary>
        /// <param name="entity">Entities</param>
        void Edit(IEnumerable<TEntity> entities);
        /// <summary>
        /// Deletes entity from the context
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity);
        /// <summary>
        /// Deletes entities from the context
        /// </summary>
        /// <param name="entity">Entities</param>
        void Delete(IEnumerable<TEntity> entities);
        /// <summary>
        /// Deletes entity or entities from the context based on given predicate
        /// </summary>
        /// <param name="predicate">where clause</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Deletes entity and related entities from the context
        /// </summary>
        /// <param name="entity">Entity</param>
        void DeleteRelatedEntities(TEntity entity);

        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns>IEnumerable of entities</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets all entities based on given predicate
        /// </summary>
        /// <param name="predicate">where clause</param>
        /// <returns>IEnumerable of entities</returns>
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Get all entities as paged
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="pageSize">page size</param>
        /// <returns>IEnumerable of entities as paged</returns>
        IEnumerable<TEntity> GetAllPaged(int page, int pageSize);
        /// <summary>
        /// Finds entity based on given predicate
        /// </summary>
        /// <param name="predicate">where clause</param>
        /// <returns>IEnumerable of entities</returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Finds entities as paged based on given predicate
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="predicate">where clause</param>
        /// <returns>IEnumerable of entities as paged</returns>
        IEnumerable<TEntity> FindPaged(int page, int pageSize, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets single entity
        /// </summary>
        /// <param name="predicate">where clause</param>
        /// <returns>Only one entity</returns>
        TEntity Find(long? id);

        /// <summary>
        /// Gets single entity
        /// </summary>
        /// <param name="predicate">where clause</param>
        /// <returns>Only one entity</returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Gets first entity
        /// </summary>
        /// <param name="predicate">where clause</param>
        /// <returns>First Entity</returns>
        TEntity First(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Gets count
        /// </summary>
        /// <returns>count of entities</returns>
        int Count();
        /// <summary>
        /// Gets count based on given criteria
        /// </summary>
        /// <param name="criteria">where clause</param>
        /// <returns>count of entities</returns>
        int Count(Expression<Func<TEntity, bool>> criteria);
    }
}
