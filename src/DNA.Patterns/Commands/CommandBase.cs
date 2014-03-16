using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Patterns.Commands
{
    /// <summary>
    /// Represents a command base class
    /// </summary>
    /// <typeparam name="TReceiver"></typeparam>
    public abstract class CommandBase<TReceiver> : ICommand where TReceiver : class
    {
        protected TReceiver Receiver { get; set; }

        public CommandBase() { Receiver = null; }

        public CommandBase(TReceiver receiver)
        {
            this.Receiver = receiver;
        }

        public abstract void Execute(object parameters = null);

        IDictionary<string, object> ICommand.Data
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }

    public abstract class CommandBase : CommandBase<object> { }
}
