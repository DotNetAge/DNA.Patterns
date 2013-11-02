//  Copyright (c) 2013 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php


namespace DNA.Web.Events
{
    /// <summary>
    /// Represent the base class of the IObserver to handle the stronly type event argument
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    public abstract class ObserverBase<TEventArgs> : IObserver
            where TEventArgs : class
    {
        ///// <summary>
        ///// Gets the event name to handle
        ///// </summary>
        //public abstract string EventName { get; }

        /// <summary>
        /// Gets the invoke order
        /// </summary>
        public virtual int Order
        {
            get { return 0; }
        }

        /// <summary>
        /// Process the event.
        /// </summary>
        /// <param name="sender">The event sender object.</param>
        /// <param name="eventArgs">The event argument object.</param>
        public abstract void Process(object sender, TEventArgs eventArgs);

        void IObserver.Process(object sender, object eventArgs = null)
        {
            if (eventArgs == null)
                this.Process(sender, null);
            else
                this.Process(sender, eventArgs as TEventArgs);
        }
    }

    public abstract class ObserverBase : ObserverBase<object> { }

}
