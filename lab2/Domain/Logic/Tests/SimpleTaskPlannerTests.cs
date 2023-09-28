using lab1.DataAccess;
using lab1.Domain.Models;

namespace lab1.Domain.Logic.Tests;

using Xunit;
using Moq;

public class SimpleTaskPlannerTests
{
    [Fact]
    public void CreatePlan_ReturnsCorrectPlan()
    {
        var mockRepository = new Mock<IWorkItemsRepository>();

        var mockItems = new[]
        {
            new WorkItem { Title = "Task 1", IsCompleted = false, Priority = Priority.High },
            new WorkItem { Title = "Task 2", IsCompleted = true, Priority = Priority.Medium, },
            new WorkItem { Title = "Task 3", IsCompleted = false, Priority = Priority.Urgent },
        };
        mockRepository.Setup(repo => repo.GetAll()).Returns(mockItems);

        // Створюємо об'єкт SimpleTaskPlanner і передаємо мок-об'єкт в конструктор
        var planner = new SimpleTaskPlanner(mockRepository.Object);

        var plan = planner.CreatePlan();

        Assert.NotNull(plan);
        
        Assert.Equal(Priority.Urgent, plan[0].Priority);
        Assert.Equal(Priority.High, plan[1].Priority);
        
        Assert.Equal(2, plan.Length);
        foreach (var task in plan)
        {
            Assert.False(task.IsCompleted);
        }
    }
}