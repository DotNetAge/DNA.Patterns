//  Copyright (c) 2013 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;

namespace DNA.Patterns.Commands
{
    /// <summary>
    /// The base class for command manifest
    /// </summary>
    public abstract class CommandManifestBase<T> : ICommandManifest,ICommand
        where T : ICommand
    {
        /// <summary>
        /// Gets commands
        /// </summary>
        public List<T> Commands { get; private set; }

        /// <summary>
        /// Gets command filters.
        /// </summary>
        public List<ICommandFilter> Filters { get; private set; }

        /// <summary>
        /// Gets executable commands
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetExecutableCommands()
        {
            var execableCmds = new List<T>();
            if (Commands != null && Filters != null)
            {
                foreach (var cmd in Commands)
                {
                    var shouldExecute = true;

                    foreach (var filter in Filters)
                    {
                        if (!filter.ShouldExecute(cmd))
                        {
                            shouldExecute = false;
                            break;
                        }
                    }

                    if (shouldExecute)
                        execableCmds.Add(cmd);
                }
            }
            return execableCmds;
        }

        /// <summary>
        /// Trigger when command error occurs.
        /// </summary>
        /// <param name="e"> The exception </param>       
        /// <param name="cmd">The command which throw error</param>
        protected virtual void OnCommandError(Exception e, T cmd) { }

        /// <summary>
        /// Before command execute.
        /// </summary>
        /// <param name="cmd">The command which executing</param>
        protected virtual void OnCommandExecuting(T cmd) { }

        /// <summary>
        /// Invoke all commands
        /// </summary>
        public virtual void Invoke()
        {
            var execableCmds = GetExecutableCommands();
            if (execableCmds.Count() > 0)
                Invoke(execableCmds);
        }

        /// <summary>
        /// Invoke filtered commands.
        /// </summary>
        /// <param name="filteredCommands">The exectuable command colleciton.</param>
        public abstract void Invoke(IEnumerable<T> filteredCommands);

        IEnumerable<ICommand> ICommandManifest.Commands
        {
            get { return this.Commands.Select(c=>(ICommand)c); }
        }

        IEnumerable<ICommandFilter> ICommandManifest.Filters
        {
            get { return this.Filters; }
        }

        void ICommandManifest.Invoke()
        {
            this.Invoke();
        }

        public IDictionary<String, Object> Data { get; set; }
        
        IDictionary<string, object> ICommand.Data
        {
            get
            {
                return this.Data;
            }
            set
            {
                this.Data = value;
            }
        }

        void ICommand.Execute()
        {
            this.Invoke();
        }

        void ICommand.OnError(Exception exception)
        {
            throw exception;
        }
    }
}
