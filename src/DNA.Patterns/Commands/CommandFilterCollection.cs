using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNA.Patterns.Commands
{
    public class CommandFilterCollection : List<ICommandFilter>, ICollection<ICommandFilter>
    {
        public bool ShouldExecute(ICommand command)
        {
            foreach (var filter in this)
            {
                if (!filter.ShouldExecute(command))
                    return false;
            }
            return true;
        }
    }
}
