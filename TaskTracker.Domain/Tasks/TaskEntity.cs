using System.Text.Json.Serialization;

namespace TaskTracker.Domain.Tasks;

public class TaskEntity
{
    [JsonInclude]
    public int Id { get; private set; }

    [JsonInclude]
    public string? Description { get; private set; }

    [JsonInclude]
    public Statuses Status { get; private set; }

    [JsonInclude]
    public DateTime CreatedAt { get; private set; }

    [JsonInclude]
    public DateTime UpdatedAt { get; private set; }

    public TaskEntity() { }


    private static int idCounter = 1;

    public TaskEntity(string description)
    {
        Id = idCounter;
        idCounter++;

        Description = description;
        Status = Statuses.Todo;

        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public int GetId() {  return Id; }

    public void UpdateDescriprion(string? description) 
    {
        Description = description;
    }

    public void UpdateDateSet()
    {
        UpdatedAt = DateTime.Now;
    }

    public void Show() 
    {
        Console.WriteLine($"Id: {Id} | Description: {Description}" +
            $" | Status: {Status} | CreatedAt: {CreatedAt} | UpdatedAt: {UpdatedAt}");
    }
    public void ChangeStatus(Statuses status) 
    {
        Status = status;
    }

}

public enum Statuses { Todo, InProgress, Done }
