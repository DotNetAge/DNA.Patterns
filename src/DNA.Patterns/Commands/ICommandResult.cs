using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Patterns
{
    public interface ICommandResult
    {
        ICommand Command { get; }
        
        Exception Error { get; }

        object Data { get; set; }
    }
}
