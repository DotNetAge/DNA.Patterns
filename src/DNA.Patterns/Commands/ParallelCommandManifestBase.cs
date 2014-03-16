//  Copyright (c) 2013 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;

namespace DNA.Patterns.Commands
{
    /// <summary>
    /// Represents a parallel command manifest base class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ParallelCommandManifestBase<T> : CommandManifestBase<T>
        where T : Command
    {
        public override void Invoke(IEnumerable<T> filteredCommands)
        {
            filteredCommands.AsParallel()
                .ForAll(cmd =>
                    {
                        try
                        {
                            OnCommandExecuting(cmd);
                            cmd.Execute();
                        }
                        catch (Exception e)
                        {
                            if (cmd != null)
                                cmd.HandleError(e);
                            OnCommandError(e, cmd);
                        }
                    });
        }
    }
}
