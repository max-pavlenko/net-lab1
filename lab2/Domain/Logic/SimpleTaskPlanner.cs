using lab1.DataAccess;
using lab1.Domain.Models;

namespace lab1.Domain.Logic;

public class SimpleTaskPlanner
{
    private readonly List<WorkItem> workItems = new List<WorkItem>();
    private readonly IWorkItemsRepository _workItemsRepo;

    public SimpleTaskPlanner(IWorkItemsRepository workItemsRepo)
    {
        _workItemsRepo = workItemsRepo;
    }

    public WorkItem[] CreatePlan(IEnumerable<WorkItem>? items = null)
    {
        var inputItems = _workItemsRepo.GetAll();
        var itemsList = (items ?? inputItems)
            .Where(item => !item.IsCompleted)
            .ToList();        

        itemsList.Sort((item1, item2) =>
        {
            int priorityComparison = item2.Priority.CompareTo(item1.Priority);
            if (priorityComparison != 0)
            {
                return priorityComparison;
            }

            int dueDateComparison = item1.DueDate.CompareTo(item2.DueDate);
            return dueDateComparison != 0 ? dueDateComparison : item1.Title.CompareTo(item2.Title);
        });

        return itemsList.ToArray();
    }
    
    public void AddWorkItem(WorkItem workItem)
    {
        workItems.Add(workItem);
    }

    public WorkItem[] GetWorkItems()
    {
        return workItems.ToArray();
    }

    public void MarkWorkItemAsCompleted(Guid workItemId)
    {
        var workItem = workItems.FirstOrDefault(item => item.Id == workItemId);
        if (workItem != null)
        {
            workItem.IsCompleted = true;
        }
    }

    public void RemoveWorkItem(Guid workItemId)
    {
        var workItemToRemove = workItems.FirstOrDefault(item => item.Id == workItemId);
        if (workItemToRemove != null)
        {
            workItems.Remove(workItemToRemove);
        }
    }
}