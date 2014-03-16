using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Patterns.Commands
{
    public enum TranscationStates
    {
        Pendding,
        Executing,
        Cancelled,
        Fail,
        Completed
    }
}
