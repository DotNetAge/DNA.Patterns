//  Copyright (c) 2013 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php


namespace DNA.Web.Events
{
    /// <summary>
    /// Represent the event observer interface.
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// Gets the invoke order of same events.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Process the event 
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="eventArgs">The event arguments</param>
        void Process(object sender, object eventArgs = null);
    }

    


}
