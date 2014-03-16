using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Patterns.Commands
{
    public class TranscationContext
    {
        public Guid TranID { get; set; }

        public TranscationStates State { get; internal set; }

        public Command Current { get; internal set; }

        public IErrorHandler ErrorHandler
        {
            get
            {
                if (Current != null)
                    return Current as IErrorHandler;
                return null;
            }
        }

        public dynamic Data { get; set; }

    }

}
