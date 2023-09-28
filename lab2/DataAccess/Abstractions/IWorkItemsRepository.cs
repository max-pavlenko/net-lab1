using lab1.Domain.Models;

namespace lab1.DataAccess;

public interface IWorkItemsRepository
{
    Guid Add(WorkItem workItem);
    WorkItem Get(Guid id);
    IEnumerable<WorkItem> GetAll();
    bool Update(WorkItem workItem);
    bool Remove(Guid id);
    void SaveChanges();
}