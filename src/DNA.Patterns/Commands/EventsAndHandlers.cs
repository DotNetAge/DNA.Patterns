//  Copyright (c) 2011 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Patterns
{
    public delegate void CommandErrorHandler(object sender, CommandErrorEventArgs e);

    public delegate void CommandExecuteHandler(object sender, CommandExecuteEventArgs e);

    /// <summary>
    /// Defines the exection event object.
    /// </summary>
    public class CommandExecuteEventArgs
    {
        /// <summary>
        /// Gets/Sets the current execution context
        /// </summary>
        public ICommandContext Context { get; set; }

        /// <summary>
        /// Gets/Sets whether cancel current execution.
        /// </summary>
        public bool Cancel { get; set; }
    }

    /// <summary>
    /// Defines the event object.
    /// </summary>
    public class CommandErrorEventArgs
    {
        /// <summary>
        /// Gets/Sets the current execution context
        /// </summary>
        public ICommandContext Context { get; set; }

        /// <summary>
        /// Gets/Sets the current error info object.
        /// </summary>
        public Exception ErrorInfo { get; set; }
    }

    public enum CommandExecuteModes
    {
        Sequence,
        Parallel
    }
}
