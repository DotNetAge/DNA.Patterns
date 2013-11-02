//  Copyright (c) 2011 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Patterns
{
    /// <summary>
    /// Defines the command execution context
    /// </summary>
    public interface ICommandContext
    {
        /// <summary>
        /// Gets/Sets the current executing command.
        /// </summary>
        ICommand Command { get; set; }
        
        /// <summary>
        /// Gets the Trace context
        /// </summary>
        TraceContext Trace { get; }
    }
}
