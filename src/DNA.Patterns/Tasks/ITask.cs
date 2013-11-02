//  Copyright (c) 2013 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

namespace DNA.Patterns.Tasks
{
    /// <summary>
    /// Deinfes the task interface for Task pattern.
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Gets/Sets the task fequency in minues.
        /// </summary>
        int Interval { get; set; }
        
        /// <summary>
        /// Gets/Sets the task message
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Run the task
        /// </summary>
        void Run();
    }
}
