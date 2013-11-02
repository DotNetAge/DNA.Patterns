//  Copyright (c) 2011 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System.Collections.Generic;
using System.Reflection;

namespace DNA.Patterns
{
    public abstract class TraceableCommandContext<TCommand> : ICommandContext
        where TCommand : ICommand
    {
        public TraceableCommandContext() : this(null) { }

        public TraceableCommandContext(object _params)
        {
            if (_params != null)
                Data = ConvertObject(_params);
            else
                Data = new Dictionary<string, object>();

            Trace = new TraceContext();
        }

        /// <summary>
        /// Gets/Sets the params objects 
        /// </summary>
        public IDictionary<string, object> Data { get; set; }

        /// <summary>
        /// Gets /Sets the current command instance.
        /// </summary>
        protected TCommand InnerCommand { get; set; }

        /// <summary>
        /// Gets the Trace context
        /// </summary>
        public TraceContext Trace { get; private set; }

        private IDictionary<string, object> ConvertObject(object data)
        {
            if (data is IDictionary<string, object>)
                return data as IDictionary<string, object>;

            var attr = BindingFlags.Public | BindingFlags.Instance;
            var dict = new Dictionary<string, object>();
            foreach (var property in data.GetType().GetProperties(attr))
            {
                if (property.CanRead)
                {
                    dict.Add(property.Name, property.GetValue(data, null));
                }
            }
            return dict;
        }

        ICommand ICommandContext.Command
        {
            get
            {
                return this.InnerCommand;
            }
            set
            {
                this.InnerCommand = (TCommand)value;
            }
        }

        TraceContext ICommandContext.Trace
        {
            get { return this.Trace; }
        }
    }
}
