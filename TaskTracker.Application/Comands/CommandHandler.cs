using System;
using System.Text.Json.Nodes;
using TaskTracker.Application.Comands.Interface;

namespace TaskTracker.Domain.Commands
{
    public class CommandHandler : ICommandHandler
    {
        private readonly TaskList _taskList;
        private readonly Dictionary<string, Action<string>> _commandMap;

        public CommandHandler()
        {
            _taskList = new TaskList();

            _commandMap = new Dictionary<string, Action<string>>
            {
                { "clear", parameter => Console.Clear() },
                { "-help", parameter => ShowHelp() },
                { "-h", parameter => ShowHelp() },
                { "add", HandleAddTask },
                { "update", HandleUpdateTask },
                { "delete", HandleDeleteTask },
                { "list", ListTasks }
            };
        }

        public void ExecuteCommand(string command, string parameter)
        {
            if (_commandMap.TryGetValue(command, out var action))
            {
                action(parameter);
                _taskList.SaveToJson();
            }
            else
            {
                Console.WriteLine("> Unknown command");
            }
        }

        private void ShowHelp()
        {
            Console.WriteLine(">    Commands:");
            Console.WriteLine(">        add [taskDescription]");
            Console.WriteLine(">        update [taskId]");
            Console.WriteLine(">        delete [taskId]");
            Console.WriteLine(">        list");
        }

        private void HandleAddTask(string parameter)
        {
            var newTask = _taskList.AddTask(parameter);
            Console.WriteLine($"Task with id [{newTask.GetId()}] created successfully");
        }

        private void HandleUpdateTask(string parameter)
        {
            if (int.TryParse(parameter, out int id) && _taskList.HasTaskWithId(id))
            {
                Console.Write("> [new description]: ");
                string? updateDescription = Console.ReadLine();

                if (!string.IsNullOrEmpty(updateDescription))
                {
                    _taskList.UpdateTask(id, updateDescription);
                    Console.WriteLine($"Task with id [{id}] updated successfully");
                }
                else
                {
                    Console.WriteLine("Invalid description");
                }
            }
            else
            {
                Console.WriteLine("Task not found or invalid id");
            }
        }

        private void HandleDeleteTask(string parameter)
        {
            if (int.TryParse(parameter, out int deleteId) && _taskList.DeleteTask(deleteId))
            {
                Console.WriteLine($"Task with id [{deleteId}] deleted successfully");
            }
            else
            {
                Console.WriteLine("Task not found or invalid id");
            }
        }

        private void ListTasks(string parameter)
        {
            foreach (var task in _taskList.ListTasks())
            {
                task.Show();
            }
        }
    }
}
