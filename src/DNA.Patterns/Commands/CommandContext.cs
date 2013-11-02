//  Copyright (c) 2011 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System.Collections.Generic;
using System;
using System.Reflection;

namespace DNA.Patterns
{
    /// <summary>
    /// The context class pass though the command execution pipe.
    /// </summary>
    public class CommandContext : TraceableCommandContext<ICommand>
    {
        public CommandContext() : base() { }
        
        public CommandContext(object _params) : base(_params) { }

        /// <summary>
        /// Gets/Sets the executing command object.
        /// </summary>
        public ICommand Command
        {
            get
            {
                return this.InnerCommand;
            }
            set
            {
                this.InnerCommand = value;
            }
        }

        /// <summary>
        /// Gets / Sets the dynamic object bag.
        /// </summary>
        public dynamic Bag { get; set; }
    }

}
