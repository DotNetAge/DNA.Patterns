using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Patterns.Commands
{
    /// <summary>
    /// Represents a command stack class to execute / undo command one by one.
    /// </summary>
    public class CommandStack
    {
        private Queue<Command> Executed { get; set; }

        private Queue<Command> Unexecuted { get; set; }

        /// <summary>
        /// Add command to stack
        /// </summary>
        /// <param name="cmds"></param>
        public void Add(params Command[] cmds)
        {
            for (int i = 0; i < cmds.Length; i++)
                this.Unexecuted.Enqueue(cmds[i]);
        }

        /// <summary>
        /// Gets the index of current executed command.
        /// </summary>
        public int Index { get; private set; }
        
        /// <summary>
        /// Gets current executing command
        /// </summary>
        public Command Current { get; private set; }

        /// <summary>
        /// Indicates whether the stack is end.
        /// </summary>
        public bool Eof
        {
            get
            {
                return Unexecuted.Count == 0;
            }
        }

        /// <summary>
        /// Indicates where the stack is on begin.
        /// </summary>
        public bool Bof
        {
            get
            {
                return Executed.Count == 0;
            }
        }

        /// <summary>
        /// Execute the next command.
        /// </summary>
        /// <param name="parameters">The command executation parameters object.</param>
        /// <returns>If executed returns true.</returns>
        public bool DoNext(object parameters = null)
        {
            if (Unexecuted.Count > 0)
            {
                Current = Unexecuted.Dequeue();
                try
                {
                    Current.Execute(parameters);
                }
                catch (Exception e)
                {
                    throw e;
                }
                Index++;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Undo the preious executed command.
        /// </summary>
        /// <returns>Returns true if undo completed</returns>
        public bool Undo()
        {
            if (Executed.Count > 0)
            {
                var cmd = Executed.Dequeue();
                try
                {
                    var undoable = cmd as ICanUndo;
                    if (undoable != null)
                        undoable.Undo();
                }
                catch (Exception e)
                {
                    throw e;
                }
                Index--;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reset and clear the pipe
        /// </summary>
        public void Reset()
        {
            Executed.Clear();
            Unexecuted.Clear();
            Index = 0;
        }

    }
}
