//  Copyright (c) 2011 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DNA.Data
{
    /// <summary>
    /// The Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>: IDisposable
        where T : class
    {
        /// <summary>
        /// Get all objects 
        /// </summary>
        /// <returns></returns>
        IQueryable<T> All();

        IQueryable<T> All(out int total, int index = 0, int size = 50);

        /// <summary>
        /// Get objects by filter expression.
        /// </summary>
        /// <param name="predicate">The filter expressions</param>
        /// <returns>Returns the IQueryable objects for this filter.</returns>
        IQueryable<T> Filter(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get objects by filtering and paging specifications.
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="sortingSelector">The default sorting field selector</param>
        /// <param name="filter">The filter expressions</param>
        /// <param name="total"></param>
        /// <param name="sortby"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        IQueryable<T> Filter<Key>(Expression<Func<T, Key>> sortingSelector, Expression<Func<T, bool>> filter, out int total, SortingOrders sortby = SortingOrders.Asc, int index = 0, int size = 50);
        
        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified the filter expression</param>
        bool Contains(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get the total objects count.
        /// </summary>
        int Count();

        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        T Find(params object[] keys);

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate"></param>
        T Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="t">Specified a new object to create.</param>
        T Create(T t);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="t">Specified a existing object to delete.</param> 
        void Delete(T t);

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        int Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        T Update(T t);
        
        /// <summary>
        /// Clear all data items.
        /// </summary>
        /// <returns>Total clear item count</returns>
        void Clear();

        /// <summary>
        /// Save all changes.
        /// </summary>
        /// <returns></returns>
        int Submit();
    }
}
