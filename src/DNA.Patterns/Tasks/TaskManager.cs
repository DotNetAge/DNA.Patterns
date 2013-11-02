using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DNA.Patterns.Tasks
{
    public class TaskManager
    {
        public TaskManager(IEnumerable<ICommandTask> tasks) { this.Tasks = tasks; }

        public IEnumerable<ICommandTask> Tasks { get; private set; }

        public void Start()
        {
            if (Tasks != null && Tasks.Count() > 0)
            {
                var taskThread = new Thread(new ParameterizedThreadStart((task) => { ((ITask)task).Run(); }));
                taskThread.IsBackground = true;
            }
        }

        public void Stop() { }
    }
}
