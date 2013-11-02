//  Copyright (c) 2011 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;

namespace DNA.Data
{
    /// <summary>
    /// Defines the IUnitOfWork interface  to make sure that when use multiple repositories, they share a single database context
    /// </summary>
    public interface IUnitOfWorks
    {
        IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;

        IQueryable<T> All<T>() where T : class;

        IQueryable<T> All<T>(out int total, int index = 0, int size = 50) where T : class;

        int Count<T>() where T : class;

        int Count<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Get object instance by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find<T>(object id) where T : class;

        T Find<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Add a new object to data context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        T Add<T>(T t) where T : class;

        /// <summary>
        /// Batch add objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        IEnumerable<T> Add<T>(IEnumerable<T> items) where T : class;

        /// <summary>
        ///  Mark the object is changes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        void Update<T>(T t) where T : class;

        /// <summary>
        /// Mark the object is delete.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        void Delete<T>(T t) where T : class;

        void Delete<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Clear all data items.
        /// </summary>
        /// <returns>Total clear item count</returns>
        void Clear<T>() where T : class;

        /// <summary>
        /// Submit changes to database.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        void Config(IConfiguration settings);
    }
}
