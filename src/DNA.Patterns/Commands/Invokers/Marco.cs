//  Copyright (c) 2014 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Patterns.Commands
{
    /// <summary>
    /// Reprsents a Macro class use to group commands
    /// </summary>
    public class Macro
    {
        /// <summary>
        /// Initialize the Macro object instance.
        /// </summary>
        public Macro()
        {
            Commands = new List<ICommand>();
            Filters = new CommandFilterCollection();
        }

        /// <summary>
        /// Initialize the Macro object instance with commands.
        /// </summary>
        /// <param name="cmd"></param>
        public Macro(params ICommand[] cmd)
            : this()
        {
            this.Add(cmd);
        }

        /// <summary>
        /// Gets all commands in current marco
        /// </summary>
        public ICollection<ICommand> Commands { get; private set; }

        /// <summary>
        /// Gets filters of marco
        /// </summary>
        public CommandFilterCollection Filters { get; private set; }

        /// <summary>
        /// Filter and execute all commands.
        /// </summary>
        /// <param name="parameters">The runtime parameters pass to commands</param>
        public virtual void Call(object parameters = null)
        {
            foreach (var cmd in Commands)
            {
                try
                {
                    if (Filters.ShouldExecute(cmd))
                        cmd.Execute(parameters);
                }
                catch (Exception e)
                {
                    var errorHandler = cmd as IErrorHandler;
                    if (errorHandler != null)
                        errorHandler.OnError(e);
                }
            }
        }

        /// <summary>
        /// Add command(s) to macro
        /// </summary>
        /// <param name="cmd">The command that add to macro</param>
        public virtual void Add(params ICommand[] cmd)
        {
            foreach (var c in cmd)
                Commands.Add(c);
        }

        /// <summary>
        /// Add dynamic command by specified Action array
        /// </summary>
        /// <param name="executionHandlers">The Action objects to initialize dynamic command</param>
        public virtual void Add(params Action[] executionHandlers)
        {
            this.Add(executionHandlers.Select(e => new Command(e)).ToArray());
        }

        /// <summary>
        /// Add dynamic command by specified Action array
        /// </summary>
        /// <param name="executionHandlers"The Action objects to initialize dynamic command></param>
        public virtual void Add(params Action<object>[] executionHandlers)
        {
            this.Add(executionHandlers.Select(e => new Command(e)).ToArray());
        }

        /// <summary>
        /// Remove the command from macro
        /// </summary>
        /// <param name="cmd">The cmmand instance that in the macro</param>
        /// <returns>Return true if removed otherwrise returns false.</returns>
        public virtual bool Remove(ICommand cmd) { return Commands.Remove(cmd); }

    }
}
