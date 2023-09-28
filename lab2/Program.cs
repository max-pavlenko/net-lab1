using lab1.DataAccess;
using lab1.Domain.Logic;
using lab1.Domain.Models;

var taskPlanner = new SimpleTaskPlanner(new FileWorkItemsRepository());

while (true)
{
    Console.WriteLine("Оберiть операцiю:");
    Console.WriteLine("[A]dd work item");
    Console.WriteLine("[B]uild a plan");
    Console.WriteLine("[M]ark work item as completed");
    Console.WriteLine("[R]emove a work item");
    Console.WriteLine("[Q]uit the app");

    var choice = Console.ReadKey().Key;
    Console.WriteLine();

    switch (choice)
    {
        case ConsoleKey.A:
            AddWorkItem(taskPlanner);
            break;
        case ConsoleKey.B:
            BuildPlan(taskPlanner);
            break;
        case ConsoleKey.M:
            MarkWorkItemAsCompleted(taskPlanner);
            break;
        case ConsoleKey.R:
            RemoveWorkItem(taskPlanner);
            break;
        case ConsoleKey.Q:
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Невiдома команда. Будь ласка, спробуйте ще раз.");
            break;
    }
}

void AddWorkItem(SimpleTaskPlanner tp)
{
    Console.WriteLine("Додавання робочого завдання:");
    WorkItem workItem = GetUserInputForWorkItem();
    tp.AddWorkItem(workItem);
    Console.WriteLine("Робоче завдання додано.");
}

void BuildPlan(SimpleTaskPlanner tp)
{
    Console.WriteLine("Побудова плану:");
    WorkItem[] inputItems = tp.GetWorkItems();
    var sortedItems = tp.CreatePlan(inputItems);

    Console.WriteLine("Впорядкованi завдання:");
    foreach (var item in sortedItems)
    {
        Console.WriteLine(item);
    }
}

void MarkWorkItemAsCompleted(SimpleTaskPlanner tp)
{
    Console.WriteLine("Вiдзначення робочого завдання як завершеного:");
    WorkItem[] workItems = tp.GetWorkItems();
    if (workItems.Length == 0)
    {
        Console.WriteLine("Немає доступних робочих завдань.");
        return;
    }

    Console.WriteLine("Оберiть номер завдання для вiдзначення як завершеного:");
    for (int i = 0; i < workItems.Length; i++)
    {
        Console.WriteLine($"[{i + 1}] {workItems[i].Title}");
    }

    if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex >= 1 &&
        selectedIndex <= workItems.Length)
    {
        tp.MarkWorkItemAsCompleted(workItems[selectedIndex - 1].Id);
        Console.WriteLine($"Завдання \"{workItems[selectedIndex - 1].Title}\" вiдзначено як завершене.");
    }
    else
    {
        Console.WriteLine("Некоректний вибiр. Будь ласка, виберiть номер зi списку.");
    }
}

void RemoveWorkItem(SimpleTaskPlanner tp)
{
    Console.WriteLine("Видалення робочого завдання:");
    WorkItem[] workItems = tp.GetWorkItems();
    if (workItems.Length == 0)
    {
        Console.WriteLine("Немає доступних робочих завдань.");
        return;
    }

    Console.WriteLine("Оберiть номер завдання для видалення:");
    for (int i = 0; i < workItems.Length; i++)
    {
        Console.WriteLine($"[{i + 1}] {workItems[i].Title}");
    }

    if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex >= 1 &&
        selectedIndex <= workItems.Length)
    {
        tp.RemoveWorkItem(workItems[selectedIndex - 1].Id);
        Console.WriteLine($"Завдання \"{workItems[selectedIndex - 1].Title}\" видалено.");
    }
    else
    {
        Console.WriteLine("Некоректний вибiр. Будь ласка, виберiть номер зi списку.");
    }
}

WorkItem GetUserInputForWorkItem()
{
    Console.Write("Назва завдання: ");
    var title = Console.ReadLine() ?? "Default Task Title";

    Console.Write("Дедлайн (у форматi dd.MM.yyyy): ");
    DateTime dueDate = DateTime.Today;
    if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None,
            out DateTime parsedDueDate))
    {
        dueDate = parsedDueDate;
    }
    else
    {
        Console.WriteLine("Некоректний формат дати. Дедлайн буде сьогоднi.");
    }

    Console.Write("Прiоритет (None, Low, Medium, High, Urgent): ");
    var priorityInput = Console.ReadLine();
    Priority priority = Priority.Low;
    if (Enum.TryParse(priorityInput, true, out Priority parsedPriority))
    {
        priority = parsedPriority;
    }
    else
    {
        Console.WriteLine("Некоректний прiоритет. Прiоритет буде встановлено в Low.");
    }

    Console.Write("Складнiсть (None, Minutes, Hours, Days, Weeks): ");
    var complexityInput = Console.ReadLine();
    Complexity complexity = Complexity.None;
    if (Enum.TryParse(complexityInput, true, out Complexity parsedComplexity))
    {
        complexity = parsedComplexity;
    }
    else
    {
        Console.WriteLine("Некоректна складнiсть. Складнiсть буде встановлено в None.");
    }

    return new WorkItem
    {
        Title = title,
        DueDate = dueDate,
        Priority = priority,
        Complexity = complexity
    };
}