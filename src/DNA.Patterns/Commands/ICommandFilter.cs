//  Copyright (c) 2011 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php


namespace DNA.Patterns.Commands
{
    /// <summary>
    /// Defines  a command filter
    /// </summary>
    public interface ICommandFilter
    {
        /// <summary>
        /// Determine the command should be execute.
        /// </summary>
        /// <param name="command">The target command instance.</param>
        /// <returns>Returns true when command should be executed.</returns>
        bool ShouldExecute(ICommand command);
    }
}
