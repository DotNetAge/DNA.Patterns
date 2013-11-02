//  Copyright (c) 2011 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;

namespace DNA.Patterns
{
    /// <summary>
    /// Reparents the command base class
    /// </summary>
    public abstract class Command<T> : ICommand
        where T : ICommandContext
    {
        /// <summary>
        /// Identity this command is thread safe
        /// </summary>
        public virtual bool IsThreadSafe { get { return true; } }

        /// <summary>
        /// Gets the command execute order
        /// </summary>
        /// <remarks>
        /// This property only avaliable in command sequence mode. 
        /// </remarks>
        public virtual int Order { get { return 0; } }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="context">The command execution context object.</param>
        public abstract void Execute(T context);

        /// <summary>
        /// Process the exception during command executing.
        /// </summary>
        /// <param name="exception"></param>
        protected virtual void OnError(Exception exception) { }

        #region Implement ICommandContext

        void ICommand.Execute(ICommandContext context)
        {
            this.Execute((T)context);
        }

        void ICommand.OnError(Exception exception)
        {
            this.OnError(exception);
        }

        bool ICommand.IsThreadSafe
        {
            get { return this.IsThreadSafe; }
        }

        int ICommand.Order
        {
            get { return this.Order; }
        }

        #endregion
    }

    /// <summary>
    /// Parents a common command base class.
    /// </summary>
    public abstract class Command : Command<CommandContext> { }
}
