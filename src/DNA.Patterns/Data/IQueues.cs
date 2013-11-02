//  Copyright (c) 2013 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;

namespace DNA.Data
{
    /// <summary>
    /// Defines the queues stoarge methods
    /// </summary>
    public interface IQueues
    {
        /// <summary>
        /// Clear the queue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Clear<T>() where T : class;

        /// <summary>
        /// Gets the item count 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        long Count<T>() where T : class;

        /// <summary>
        /// Dequeue entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Dequeue<T>() where T : class;

        /// <summary>
        /// Enqueue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Enqueue<T>(T entity) where T : class;

        bool IsEmpty<T>() where T : class;

        T Peek<T>() where T : class;
    }
}
