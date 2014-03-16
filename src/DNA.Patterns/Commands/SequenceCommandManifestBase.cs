//  Copyright (c) 2013 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DNA.Patterns.Commands
{
    /// <summary>
    /// Represents a mainfest that use to invoke the commands in sequence.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SequenceCommandManifestBase<T> : CommandManifestBase<T>
        where T:Command
    {
        //public override IEnumerable<T> GetExecutableCommands()
        //{
        //    var cmds = base.GetExecutableCommands();
        //    if (cmds.Count() > 0)
        //    {
        //        var orderedCmds = cmds.OrderBy(c => c.Order).ToList();
        //        return orderedCmds;
        //    }
        //    return cmds;
        //}

        /// <summary>
        /// Gets/Sets stop the execute sequence when error occurs.
        /// </summary>
        public bool StopOnError { get; set; }

        /// <summary>
        /// Invoke all executable commands and keep the execution thead safe.
        /// </summary>
        /// <param name="filteredCommands">The executable commands.</param>
        public override void Invoke(IEnumerable<T> filteredCommands)
        {
            //foreach (var cmd in filteredCommands)
            //{
            //    try
            //    {
            //        Monitor.Enter(cmd);
            //        OnCommandExecuting(cmd);
            //    }
            //    catch (Exception e)
            //    {
            //        if (cmd != null)
            //            cmd.OnError(e);

            //        OnCommandError(e, cmd);

            //        if (StopOnError)
            //            return;

            //        continue;
            //    }
            //    finally
            //    {
            //        Monitor.Exit(cmd);
            //    }
            //}
        }
    }
}
