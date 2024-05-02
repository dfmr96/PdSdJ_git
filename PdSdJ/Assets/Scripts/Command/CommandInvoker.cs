using System.Collections.Generic;

namespace Command
{
    public class CommandInvoker
    {
        private List<ICommand> _commands = new List<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }
    }
}