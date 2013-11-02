//  Copyright (c) 2013 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using DNA.Patterns.Commands;
using System;

namespace DNA.Patterns.Tasks
{
    /// <summary>
    /// The Task pattern combine with Command pattern
    /// </summary>
    public interface ICommandTask : ITask
    {
        /// <summary>
        /// Gets the command that execute on task successful run.
        /// </summary>
        ICommand SuccessCommand { get; }

        /// <summary>
        /// Gets the command that execute on fail.
        /// </summary>
        ICommand FailCommand { get; }

        /// <summary>
        /// Gets the process command
        /// </summary>
        ICommand Command { get; }

        /// <summary>
        /// Execute the fail command.
        /// </summary>
        /// <param name="e"></param>
        void Fail(Exception e);

        /// <summary>
        /// Execute the success command.
        /// </summary>
        void Succeed();

    }

}
