using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Patterns
{
    /// <summary>
    /// Represents the interface use to hande error.
    /// </summary>
    public interface IErrorHandler
    {
        void OnError(Exception e);
    }
}
