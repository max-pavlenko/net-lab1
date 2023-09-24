namespace lab1.Domain.Models;

using System;

public class WorkItem
{
    DateTime CreationDate { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public Complexity Complexity { get; set; }
    public string Title { get; set; }
    string Description { get; set; }
    bool IsCompleted { get; set; }

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

    // Override the ToString() method
    public override string ToString()
    {
        string formattedDueDate = DueDate.ToString("dd.MM.yyyy");
        string formattedPriority = char.ToUpper(Priority.ToString()[0]) + Priority.ToString().Substring(1);
        return $"{Title}: due {formattedDueDate}, {formattedPriority} priority";
    }
}