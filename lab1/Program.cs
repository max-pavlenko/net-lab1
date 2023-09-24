// See https://aka.ms/new-console-template for more information

// Користувач вводить довільну кількість WorkItem

using lab1.Domain.Logic;
using lab1.Domain.Models;

Console.WriteLine("Введіть ваші завдання (для завершення введення, натисніть Enter без тексту назви завдання):");
WorkItem[] inputItems = GetUserInput();

SimpleTaskPlanner taskPlanner = new SimpleTaskPlanner();

var sortedItems = taskPlanner.CreatePlan(inputItems);

Console.WriteLine("Впорядковані завдання:");
foreach (var item in sortedItems)
{
    Console.WriteLine(item);
}

WorkItem[] GetUserInput()
{
    var inputItems = new List<WorkItem>();
    while (true)
    {
        Console.Write("Назва завдання: ");
        var title = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(title))
        {
            break;
        }

        var item = new WorkItem { Title = title };
        var random = new Random();

        Console.Write("Дедлайн (у форматі dd.MM.yyyy): ");
        if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None,
                out DateTime dueDate))
        {
            item.DueDate = dueDate;
        }
        else
        {
            Console.WriteLine("Некоректний формат дати. Дедлайн буде сьогодні.");
        }

        Console.Write("Пріоритет (None, Low, Medium, High, Urgent): ");
        if (Enum.TryParse(Console.ReadLine(), ignoreCase: true, out Priority priority))
        {
            item.Priority = priority;
        }
        else
        {
            Console.WriteLine("Некоректний пріоритет. Пріоритет буде рандомний.");
            var randomPriority = (Priority)random.Next(Enum.GetValues(typeof(Priority)).Length);
            item.Priority = randomPriority;
        }

        Console.Write("Складність (None, Minutes, Hours, Days, Weeks): ");
        if (Enum.TryParse(Console.ReadLine(), ignoreCase: true, out Complexity complexity))
        {
            item.Complexity = complexity;
        }
        else
        {
            Console.WriteLine("Некоректна складність. Складність буде рандомна.");
            var randomComplexity = (Complexity)random.Next(Enum.GetValues(typeof(Complexity)).Length);
            item.Complexity = randomComplexity;
        }

        inputItems.Add(item);
    }

    return inputItems.ToArray();
}