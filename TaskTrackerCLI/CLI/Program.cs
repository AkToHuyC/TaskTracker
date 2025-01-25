using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application.App.Interface;
using TaskTracker.Application.Comands.Interface;
using TaskTracker.Domain.Commands;
using TaskTracker.Domain.Tasks;

namespace TaskTracker.Infrastructure.CLI
{
    public class Program
    {
        static void Main()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ICommandHandler, CommandHandler>();
            services.AddSingleton<ITaskTrackerApp, TaskTrackerApp>();

            var serviceProvider = services.BuildServiceProvider();

            var app = serviceProvider.GetService<ITaskTrackerApp>();

            app?.Run();
            //TODO:
            //Отметить задачу как выполняемую или выполненную
            //Перечислите все выполненные задачи.
            //Перечислите все задачи, которые не выполнены
            //Перечислите все задачи, которые находятся в процессе выполнения.
        }
    }
}
