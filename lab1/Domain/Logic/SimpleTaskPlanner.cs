using lab1.Domain.Models;

namespace lab1.Domain.Logic;

public class SimpleTaskPlanner
{
    public WorkItem[] CreatePlan(WorkItem[] items)
    {
        List<WorkItem> itemsList = items.ToList();

        // Сортуємо за заданими критеріями
        itemsList.Sort((item1, item2) =>
        {
            int priorityComparison = item2.Priority.CompareTo(item1.Priority);
            if (priorityComparison != 0)
            {
                return priorityComparison;
            }

            int dueDateComparison = item1.DueDate.CompareTo(item2.DueDate);
            if (dueDateComparison != 0)
            {
                return dueDateComparison;
            }

            return item1.Title.CompareTo(item2.Title);
        });

        return itemsList.ToArray();
    }
}