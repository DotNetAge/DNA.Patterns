//  Copyright (c) 2014 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNA.Patterns.Commands
{
    public class Transcation : IDisposable
    {
        public TranscationContext Context { get; private set; }

        protected Queue<Command> Executed { get; set; }

        protected Queue<Command> Unexecuted { get; set; }

        public void Add(params Command[] cmds)
        {
            for (int i = 0; i < cmds.Length; i++)
                this.Unexecuted.Enqueue(cmds[i]);
        }

        public void Begin(object parameters = null)
        {
            Context = new TranscationContext()
            {
                TranID = Guid.NewGuid(),
                State = TranscationStates.Executing
            };

            while (Unexecuted.Count > 0)
            {
                try
                {
                    Context.Current = Unexecuted.Dequeue();
                    Context.Current.Execute(parameters);
                }
                catch (Exception e)
                {
                    if (Context.ErrorHandler != null)
                        Context.ErrorHandler.OnError(e);
                }

                Executed.Enqueue(Context.Current);
            }

        }

        public void Commit()
        {
            if (Context != null)
                Context.State = TranscationStates.Completed;
        }

        public void Rollback()
        {
            while (Executed.Count > 0)
            {
                try
                {
                    Context.Current = Executed.Dequeue();
                    var undoable = Context.Current as ICanUndo;
                    if (undoable != null) 
                    undoable.Undo();
                }
                catch (Exception e)
                {
                    if (Context.ErrorHandler != null)
                        Context.ErrorHandler.OnError(e);
                }

                Unexecuted.Enqueue(Context.Current);
            }

            Context.State = TranscationStates.Cancelled;
        }

        public void Dispose()
        {
            this.Unexecuted.Clear();
            this.Executed.Clear();

            if (Context != null && Context.State == TranscationStates.Executing)
                this.Commit();
        }
    }
}
