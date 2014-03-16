//  Copyright (c) 2014 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Patterns.Commands
{
    /// <summary>
    /// Represents a command table class used to executed commands by name.
    /// </summary>
    public class CommandTable
    {
        /// <summary>
        /// Initialize the CommandTable object instance.
        /// </summary>
        public CommandTable()
        {
            CommandSet = new Dictionary<string, Macro>();
        }

        /// <summary>
        /// Gets the marcos of the command table.
        /// </summary>
        public Dictionary<string, Macro> CommandSet { get; private set; }

        /// <summary>
        /// Add commands and group into a macro by name.
        /// </summary>
        /// <param name="commandName">The command group name.</param>
        /// <param name="commands">The commands add to the naming macro</param>
        public void Add(string commandName, params ICommand[] commands)
        {
            if (this.CommandSet[commandName] != null)
            {
                this.CommandSet[commandName].Add(commands);
            }
            else
            {
                var macro = new Macro();
                macro.Add(commands);
                this.CommandSet.Add(commandName, macro);
            }
        }

        /// <summary>
        /// Execute commands by specified name.
        /// </summary>
        /// <param name="commandName">The command group name.</param>
        /// <param name="parameters">The runtime parameters object that pass to all commands.</param>
        public void Run(string commandName, object parameters = null)
        {
            var macro = this.CommandSet[commandName];
            macro.Call(parameters);
        }
    }
}
