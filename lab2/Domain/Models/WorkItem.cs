using Newtonsoft.Json;

namespace lab1.Domain.Models;

using System;

public class WorkItem
{
    public Guid Id { get; set; }
    DateTime CreationDate { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public Complexity Complexity { get; set; }
    public string Title { get; set; }
    string Description { get; set; }
    public bool IsCompleted { get; set; }

    // Constructor
    public WorkItem()
    {
        CreationDate = DateTime.Now;
        DueDate = DateTime.Now;
        Priority = Priority.None;
        Complexity = Complexity.None;
        Title = "";
        Description = "";
        IsCompleted = false;
    }

    public override string ToString()
    {
        string formattedDueDate = DueDate.ToString("dd.MM.yyyy");
        string formattedPriority = char.ToUpper(Priority.ToString()[0]) + Priority.ToString().Substring(1);
        string formattedComplexity = char.ToUpper(Complexity.ToString()[0]) + Complexity.ToString().Substring(1);
        return $"{Title}: due {formattedDueDate}, {formattedPriority} priority, {formattedComplexity} complexity," +
               $" is completed - {IsCompleted}";
    }

    public WorkItem Clone()
    {
        var serializedWorkItem = JsonConvert.SerializeObject(this);
        var clonedWorkItem = JsonConvert.DeserializeObject<WorkItem>(serializedWorkItem)!;

        return clonedWorkItem;
    }
}