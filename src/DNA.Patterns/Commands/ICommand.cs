//  Copyright (c) 2011 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;

namespace DNA.Patterns.Commands
{
    /// <summary>
    /// Defines the Command interface
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets/Sets the command data object
        /// </summary>
        /// <remarks>
        /// This object can be used to store the additional parameters on executing.
        /// </remarks>
        IDictionary<string, Object> Data { get; set; }

        /// <summary>
        /// Execute a command 
        /// </summary>
        void Execute();

        /// <summary>
        /// Throw and exception on command execution.
        /// </summary>
        /// <param name="exception">The exception object</param>
        void OnError(Exception exception);
    }

    /// <summary>
    /// Represents an ordered command interface that using in execution sequences.
    /// </summary>
    public interface IOrderedCommand:ICommand
    {
        /// <summary>
        /// Gets/Sets the command execute order
        /// </summary>
        /// <remarks>
        /// This property only avaliable in command sequence mode. 
        /// </remarks>
        int Order { get; set; }
    }

}
