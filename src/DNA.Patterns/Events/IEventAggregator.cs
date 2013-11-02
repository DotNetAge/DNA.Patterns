//  Copyright (c) 2013 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Web.Events
{
  
    /// <summary>
    /// Represent the event arggregator to publish and invoke events.
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// Publish the event
        /// </summary>
        /// <param name="name">The event name.</param>
        /// <param name="sender">The sender that trigger this event</param>
        /// <param name="eventArgs">The event arguments</param>
        void Publish(string name, object sender, object eventArgs = null);

        /// <summary>
        /// Get all observers by specified event name.
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        IEnumerable<IObserver> GetObservers(string eventName);
    }


}
