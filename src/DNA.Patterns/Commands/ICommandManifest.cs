//  Copyright (c) 2011 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System.Collections.Generic;

namespace DNA.Patterns.Commands
{
    /// <summary>
    /// Defines the command container methods.
    /// </summary>
    public interface ICommandManifest
    {
        /// <summary>
        /// Gets get all commands of this manifest.
        /// </summary>
        IEnumerable<ICommand> Commands { get; }
        
        /// <summary>
        /// Gets the command filter instances in container.
        /// </summary>
        IEnumerable<ICommandFilter> Filters { get; }

        /// <summary>
        /// Invoke all commmands.
        /// </summary>
        void Invoke();
    }

}
