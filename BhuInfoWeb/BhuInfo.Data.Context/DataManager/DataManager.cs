using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pms.Data
{
    public abstract class DataManager<T, K> where T : class
    {
        protected DbContext Context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public DataManager(DbContext context)
        {
            Context = context;
        }


        /// <summary>
        ///     Returns a single object with a primary key of the provided id
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        public T Get(K id)
        {
            return Context.Set<T>().Find(id);
        }


        /// <summary>
        ///     Returns a single object with a primary key of the provided id
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="id">The primary key of the object to fetch</param>
        /// <returns>A single object with the provided primary key or null</returns>
        public async Task<T> GetAsync(K id)
        {
            return await Context.Set<T>().FindAsync(id);
        }


        /// <summary>
        ///     Gets a collection of all objects in the database
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <returns>An ICollection of every object in the database</returns>
        public ICollection<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        /// <summary>
        ///     Gets a collection of all objects in the database
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <returns>An ICollection of every object in the database</returns>
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        /// <summary>
        ///     Returns a single object which matches the provided expression
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="match">A Linq expression filter to find a single result</param>
        /// <returns>
        ///     A single object which matches the expression filter.
        ///     If more than one object is found or if zero are found, null is returned
        /// </returns>
        public T Find(Expression<Func<T, bool>> match)
        {
            return Context.Set<T>().SingleOrDefault(match);
        }

        /// <summary>
        ///     Returns a single object which matches the provided expression
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="match">A Linq expression filter to find a single result</param>
        /// <returns>
        ///     A single object which matches the expression filter.
        ///     If more than one object is found or if zero are found, null is returned
        /// </returns>
        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await Context.Set<T>().SingleOrDefaultAsync(match);
        }

        /// <summary>
        ///     Returns a collection of objects which match the provided expression
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="match">A linq expression filter to find one or more results</param>
        /// <returns>An ICollection of object which match the expression filter</returns>
        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return Context.Set<T>().Where(match).ToList();
        }

        /// <summary>
        ///     Returns a collection of objects which match the provided expression
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="match">A linq expression filter to find one or more results</param>
        /// <returns>An ICollection of object which match the expression filter</returns>
        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await Context.Set<T>().Where(match).ToListAsync();
        }


        /// <summary>
        ///     Inserts a single object to the database and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="t">The object to insert</param>
        /// <returns>The resulting object including its primary key after the insert</returns>
        public T Add(T t)
        {
            Context.Set<T>().Add(t);
            Context.SaveChanges();
            return t;
        }

        /// <summary>
        ///     Inserts a single object to the database and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="t">The object to insert</param>
        /// <returns>The resulting object including its primary key after the insert</returns>
        public async Task<T> AddAsync(T t)
        {
            Context.Set<T>().Add(t);
            await Context.SaveChangesAsync();
            return t;
        }

        /// <summary>
        ///     Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="tList">An IEnumerable list of objects to insert</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
        public IEnumerable<T> AddAll(IEnumerable<T> tList)
        {
            var addAll = tList as IList<T> ?? tList.ToList();
            Context.Set<T>().AddRange(addAll);
            Context.SaveChanges();
            return addAll;
        }

        /// <summary>
        ///     Inserts a collection of objects into the database and commits the changes
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="tList">An IEnumerable list of objects to insert</param>
        /// <returns>The IEnumerable resulting list of inserted objects including the primary keys</returns>
        public async Task<IEnumerable<T>> AddAllAsync(IEnumerable<T> tList)
        {
            Context.Set<T>().AddRange(tList);
            await Context.SaveChangesAsync();
            return tList;
        }

        /// <summary>
        ///     Adds or Updates a single object based on the provided primary key and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="obj">The  object to add or update to the database</param>
        /// <returns>The resulting updated object</returns>
        public T AddOrUpdate(T obj, object key)
        {
            var existing = Context.Set<T>().Find(key);
            if (existing == null)
            {
                existing = obj;
                Context.Set<T>().Add(existing);
            }
            else
            {
                Context.Entry(existing).CurrentValues.SetValues(obj);
            }

            //ObjectContext objectContext = ((IObjectContextAdapter)Context).ObjectContext;
            //ObjectStateEntry stateEntry = null;
            //objectContext.ObjectStateManager.TryGetObjectStateEntry(obj, out stateEntry);

            //var objectSet = objectContext.CreateObjectSet<T>();
            //if (stateEntry == null || stateEntry.EntityKey.IsTemporary)
            //{
            //    objectSet.AddObject(obj);
            //}

            //else if (stateEntry.State == EntityState.Detached)
            //{
            //    objectSet.Attach(obj);
            //    objectContext.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            //}
            Context.SaveChanges();
            return existing;
        }


        /// <summary>
        ///     Adds or Updates a single object based on the provided primary key and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="obj">The object to apply to the database</param>
        /// <returns>The resulting updated object</returns>
        public async Task<T> AddOrUpdateAsync(T obj, object key)
        {
            var existing = Context.Set<T>().Find(key);
            if (existing == null)
                Context.Set<T>().Add(obj);
            else
                Context.Entry(obj).State = EntityState.Modified;
            //    ObjectContext objectContext = ((IObjectContextAdapter)Context).ObjectContext;
            //ObjectStateEntry stateEntry = null;
            //objectContext.ObjectStateManager.TryGetObjectStateEntry(obj, out stateEntry);

            //var objectSet = objectContext.CreateObjectSet<T>();
            //if (stateEntry == null || stateEntry.EntityKey.IsTemporary)
            //{
            //    objectSet.AddObject(obj);
            //}

            //else if (stateEntry.State == EntityState.Detached)
            //{
            //    objectSet.Attach(obj);
            //    objectContext.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            //}
            await Context.SaveChangesAsync();
            return obj;
        }


        /// <summary>
        ///     Updates a single object based on the provided primary key and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="updated">The updated object to apply to the database</param>
        /// <param name="key">The int primary key of the object to update</param>
        /// <returns>The resulting updated object</returns>
        public T Update(T updated, K key)
        {
            if (updated == null)
                return null;

            var existing = Context.Set<T>().Find(key);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
                Context.SaveChanges();
            }
            return existing;
        }


        /// <summary>
        ///     Updates a single object based on the provided primary key and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="updated">The updated object to apply to the database</param>
        /// <param name="key">The int primary key of the object to update</param>
        /// <returns>The resulting updated object</returns>
        public async Task<T> UpdateAsync(T updated, K key)
        {
            if (updated == null)
                return null;

            var existing = await Context.Set<T>().FindAsync(key);
            if (existing != null)
            {
                Context.Entry(existing).CurrentValues.SetValues(updated);
                await Context.SaveChangesAsync();
            }
            return existing;
        }


        /// <summary>
        ///     Deletes a single object from the database and commits the change
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <param name="t">The object to delete</param>
        public void Delete(T t)
        {
            Context.Set<T>().Remove(t);
            Context.SaveChanges();
        }

        /// <summary>
        ///     Deletes a single object from the database and commits the change
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <param name="t">The object to delete</param>
        public async Task<int> DeleteAsync(T t)
        {
            Context.Set<T>().Remove(t);
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Gets the count of the number of objects in the databse
        /// </summary>
        /// <remarks>Synchronous</remarks>
        /// <returns>The count of the number of objects</returns>
        public int Count()
        {
            return Context.Set<T>().Count();
        }

        /// <summary>
        ///     Gets the count of the number of objects in the databse
        /// </summary>
        /// <remarks>Asynchronous</remarks>
        /// <returns>The count of the number of objects</returns>
        public async Task<int> CountAsync()
        {
            return await Context.Set<T>().CountAsync();
        }
    }
}