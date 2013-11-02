//  Copyright (c) 2011 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DNA.Patterns
{
    /// <summary>
    /// Reparents the command container base class
    /// </summary>
    public class CommandContainer<TCmd, TFilter> : ICommandContainer, IDisposable
        where TCmd : ICommand
        where TFilter : ICommandFilter
    {
        public CommandContainer()
        {
            InnerItems = new List<TCmd>();
            InnerFilters = new List<TFilter>();
        }

        /// <summary>
        /// Stop execute the commands when error occurs
        /// </summary>
        public bool StopOnError { get; set; }

        /// <summary>
        /// Gets/Sets the container stop the execution commands when cancel.
        /// </summary>
        public bool StopOnCancel { get; set; }

        private ICommandContext currentContext = null;

        /// <summary>
        /// Trigger before command execute.
        /// </summary>
        public event CommandExecuteHandler Execute;

        /// <summary>
        /// Trigger when command error occurs.
        /// </summary>
        public event CommandErrorHandler Error;

        /// <summary>
        /// Gets/Sets the command execution mode.
        /// </summary>
        public CommandExecuteModes Mode { get; set; }

        /// <summary>
        /// Gets the trace data 
        /// </summary>
        /// <returns>Returns the trace data collection.</returns>
        public IEnumerable<TraceData> GetTraceResult()
        {
            if ((currentContext != null) && (currentContext.Trace != null) && (currentContext.Trace.TraceData != null))
                return currentContext.Trace.TraceData;

            return new List<TraceData>();
        }

        private void Invoke(IEnumerable<ICommand> commands, IEnumerable<ICommandFilter> filters, ICommandContext context)
        {
            var execCmds = commands;
            if ((filters != null) && (filters.Count() > 0))
                execCmds = GetExecutableCommands(commands, filters);

            currentContext = context;

            if (Mode == CommandExecuteModes.Sequence)
            {
                foreach (var cmd in execCmds)
                {
                    try
                    {
                        if (!cmd.IsThreadSafe) Monitor.Enter(cmd);
                        currentContext.Command = cmd;

                        if (!OnCommandExecute(currentContext))
                            cmd.Execute(currentContext);
                        else
                        {
                            if (StopOnCancel)
                                return;
                        }
                    }
                    catch (Exception e)
                    {
                        if (cmd != null)
                            cmd.OnError(e);

                        OnCommandError(currentContext, e);

                        if (StopOnError) return;

                        continue;
                    }
                    finally
                    {
                        if (!cmd.IsThreadSafe) Monitor.Exit(cmd);
                    }
                }
            }
            else
            {
                execCmds.AsParallel()
                    .ForAll(cmd =>
                    {
                        try
                        {
                            currentContext.Command = cmd;
                            if (!OnCommandExecute(currentContext))
                                cmd.Execute(currentContext);
                        }
                        catch (Exception e)
                        {
                            if (cmd != null)
                                cmd.OnError(e);
                            OnCommandError(currentContext, e);
                        }
                    });
            }
        }

        private bool ShouldExecute(ICommand command, IEnumerable<ICommandFilter> filters)
        {
            foreach (var filter in filters)
            {
                if (!filter.ShouldExecute(command))
                    return false;
            }
            return true;
        }

        protected void Invoke(ICommandContext context)
        {
            Invoke(InnerItems.Cast<ICommand>(), InnerFilters.Cast<ICommandFilter>(), context);
        }

        /// <summary>
        /// Trigger when command error occurs.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCommandError(CommandErrorEventArgs e) { }

        /// <summary>
        /// Before command execute.
        /// </summary>
        /// <remarks>
        /// Set <seealso cref="CommandExecuteEventArgs"/> Cancel property to cancel the command execution.
        /// </remarks>
        /// <param name="e"></param>
        protected virtual void OnCommandExecute(CommandExecuteEventArgs e) { }

        /// <summary>
        /// Return false to cancel execution.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool OnCommandExecute(ICommandContext context)
        {
            var e = new CommandExecuteEventArgs()
            {
                Context = context,
                Cancel = false
            };

            OnCommandExecute(e);

            if (Execute != null)
                Execute(this, e);

            return e.Cancel;
        }

        private void OnCommandError(ICommandContext context, Exception exception)
        {
            var e = new CommandErrorEventArgs()
            {
                Context = context,
                ErrorInfo = exception
            };

            OnCommandError(e);

            if (Error != null)
                Error(this, e);
        }

        protected IEnumerable<ICommand> GetExecutableCommands(IEnumerable<ICommand> commands, IEnumerable<ICommandFilter> filters)
        {
            return commands.Where(cmd => ShouldExecute(cmd, filters)).OrderBy(cmd => cmd.Order);
        }

        protected List<TCmd> InnerItems { get; private set; }

        protected List<TFilter> InnerFilters { get; private set; }

        #region Implement ICommandContainer

        IEnumerable<ICommand> ICommandContainer.Commands
        {
            get { return this.InnerItems.Cast<ICommand>(); }
        }

        IEnumerable<ICommandFilter> ICommandContainer.Filters
        {
            get { return this.InnerFilters.Cast<ICommandFilter>(); }
        }

        void ICommandContainer.Invoke(ICommandContext context)
        {
            this.Invoke(context);
        }

        #endregion

        public void Dispose()
        {
            if (InnerFilters != null)
            {
                InnerFilters.Clear();
                InnerFilters = null;
            }

            if (InnerItems != null)
            {
                InnerItems.Clear();
                InnerItems = null;
            }
            currentContext = null;
            GC.SuppressFinalize(this);
        }
    }

    public class CommandContainer : CommandContainer<ICommand, ICommandFilter>
    {
        public CommandContainer() : base() { }
    }
}
