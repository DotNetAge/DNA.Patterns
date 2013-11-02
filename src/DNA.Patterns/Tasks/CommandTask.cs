//  Copyright (c) 2013 Ray Liang (http://www.dotnetage.com)
//  Licensed MIT: http://www.opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DNA.Patterns.Commands;

namespace DNA.Patterns.Tasks
{
    public class CommandTask : ICommandTask
    {
        public CommandTask(ICommand command) { this.Command = command; }

        public CommandTask(ICommand command, ICommand successCommand) : this(command) { this.SuccessCommand = successCommand; }

        public CommandTask(ICommand command, ICommand successCommand, ICommand failCommand) : this(command, successCommand) { this.FailCommand = failCommand; }

        public ICommand SuccessCommand { get; set; }

        public ICommand FailCommand { get; set; }

        public ICommand Command { get; set; }

        public int Interval { get; set; }

        public string Message { get; set; }

        public void Fail(Exception e)
        {
            if (FailCommand != null)
            {
                FailCommand.Data = new Dictionary<String, object>();
                FailCommand.Data.Add("Error", e);
                FailCommand.Data.Add("Task", this);
                FailCommand.Execute();
            }
        }

        public void Succeed()
        {
            if (SuccessCommand != null)
            {
                SuccessCommand.Data = new Dictionary<String, object>();
                SuccessCommand.Data.Add("Task", this);
                SuccessCommand.Execute();
            }
        }

        public void Run()
        {
            try
            {
                if (Command != null)
                {
                    Command.Data = new Dictionary<String, object>();
                    Command.Data.Add("Task", this);
                    Command.Execute();
                }
            }
            catch (Exception e)
            {
                Fail(e);
            }

            Succeed();
        }

    }
}
