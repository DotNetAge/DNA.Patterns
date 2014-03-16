using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Patterns.Commands
{
    /// <summary>
    /// Represents the undoable interface.
    /// </summary>
    public interface ICanUndo
    {
        /// <summary>
        /// Undo command
        /// </summary>
        void Undo();
    }
}
