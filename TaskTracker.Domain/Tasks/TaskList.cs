using System.Text.Json;
using TaskTracker.Domain.Tasks;

public class TaskList
{
    private readonly List<TaskEntity> tasks = [];
    private const string JsonFilePath = "tasks.json";

    public TaskList()
    {
        LoadFromJson();
    }

    public TaskEntity AddTask(string description)
    {
        var newTask = new TaskEntity(description);
        tasks.Add(newTask);
        return newTask;
    }

    public bool DeleteTask(int id)
    {
        var task = tasks.Find(t => t.GetId() == id);
        return task != null && tasks.Remove(task);
    }

    public void UpdateTask(int id, string description)
    {
        var task = tasks.Find(t => t.GetId() == id);
        if (task != null) 
        {
            task.UpdateDateSet();
            task.UpdateDescriprion(description);
        }
    }

    public bool HasTaskWithId(int id)
    {
        var task = tasks.Find(t => t.GetId() == id);
        if (task != null)
        {
            return true;
        }
        return false;
    }

    public IEnumerable<TaskEntity> ListTasks()
    {
        return tasks;
    }

    public void SaveToJson()
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(JsonFilePath, json);
    }

    public void LoadFromJson()
    {
        if (File.Exists(JsonFilePath))
        {
            var json = File.ReadAllText(JsonFilePath);

            if (!string.IsNullOrWhiteSpace(json))
            {
                var loadedTasks = JsonSerializer.Deserialize<List<TaskEntity>>(json);

                if (loadedTasks != null)
                {
                    tasks.Clear();
                    tasks.AddRange(loadedTasks);
                }
            }
        }
    }
}