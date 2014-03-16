using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Patterns.Commands
{
    public class Command<TReceiver> : CommandBase<TReceiver>, ICanUndo, IErrorHandler
    where TReceiver : class
    {
        public Command() : base() { }
        
        public Command(Action executionHandler)
        {
            this.Receiver = null;
            this.ExecutionHandler = new Action<object>((o) => { executionHandler.Invoke(); });
        }

        public Command(TReceiver receiver) : base(receiver) { }

        public Command(Action<object> executionHandler)
            : base()
        {
            this.ExecutionHandler = executionHandler;
        }

        public Command(Action<object> executionHandler, Action<Exception> errorHandler)
            : this(executionHandler)
        {
            this.ErrorHandler = errorHandler;
        }

        public override void Execute(object parameters = null)
        {
            if (ExecutionHandler == null)
                this.OnExecute(parameters);
            else
                this.ExecutionHandler(parameters);
        }

        public virtual void Undo() { }

        public void HandleError(Exception e)
        {
            if (ErrorHandler == null)
                this.OnError(e);
            else
                this.ErrorHandler(e);
        }

        public Action<object> ExecutionHandler { get; set; }

        public Action<Exception> ErrorHandler { get; set; }

        protected virtual void OnError(Exception e) { }

        protected virtual void OnExecute(object parameters = null) { }


        void ICanUndo.Undo()
        {
            this.Undo();
        }

        void IErrorHandler.OnError(Exception e)
        {
            this.OnError(e);
        }
    }

    public class Command : Command<object>
    {
        public Command() : base() { }

        public Command(object receiver) : base(receiver) { }
        
        public Command(Action executionHandler):base(executionHandler) {  }

        public Command(Action<object> executionHandler)
            : base()
        {
            this.ExecutionHandler = executionHandler;
        }

        public Command(Action<object> executionHandler, Action<Exception> errorHandler)
            : base( executionHandler)
        {
        }
    }
}
