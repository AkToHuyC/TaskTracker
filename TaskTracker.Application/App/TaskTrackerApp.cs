using System;
using TaskTracker.Application.App.Interface;
using TaskTracker.Application.Comands.Interface;
using TaskTracker.Domain.Commands;

namespace TaskTracker
{
    public class TaskTrackerApp : ITaskTrackerApp
    {
        private readonly ICommandHandler _commandHandler;

        public TaskTrackerApp(ICommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public void Run()
        {
            Console.WriteLine("TaskTracker");

            while (true)
            {
                Console.Write("> ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                ProcessInput(input);
            }
        }

        private void ProcessInput(string input)
        {
            var parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            var command = parts[0].ToLower();
            var parameter = parts.Length > 1 ? parts[1] : string.Empty;

            _commandHandler.ExecuteCommand(command, parameter);
        }
    }
}
