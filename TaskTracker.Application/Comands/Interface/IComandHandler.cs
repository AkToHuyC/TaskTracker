namespace TaskTracker.Application.Comands.Interface;

public interface ICommandHandler
{
    void ExecuteCommand(string command, string parameter);
}
