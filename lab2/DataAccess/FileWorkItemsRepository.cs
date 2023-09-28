using lab1.Domain.Models;
using Newtonsoft.Json;

namespace lab1.DataAccess;

public class FileWorkItemsRepository : IWorkItemsRepository
{
    private const string FileName = "work-items.json";
    private readonly Dictionary<Guid, WorkItem> _workItems;

    public FileWorkItemsRepository()
    {
        _workItems = LoadWorkItemsFromFile();
    }

    public Guid Add(WorkItem workItem)
    {
        var id = Guid.NewGuid();
        workItem.Id = id;
        _workItems[id] = workItem;
        SaveChanges();
        return id;
    }

    public WorkItem Get(Guid id)
    {
        if (_workItems.TryGetValue(id, out var workItem))
        {
            return workItem;
        }
        return null;
    }

    public IEnumerable<WorkItem> GetAll()
    {
        return _workItems.Values;
    }

    public bool Update(WorkItem workItem)
    {
        if (_workItems.ContainsKey(workItem.Id))
        {
            _workItems[workItem.Id] = workItem;
            SaveChanges();
            return true;
        }
        return false;
    }

    public bool Remove(Guid id)
    {
        if (_workItems.Remove(id))
        {
            SaveChanges();
            return true;
        }
        return false;
    }

    public void SaveChanges()
    {
        var serializedWorkItems = JsonConvert.SerializeObject(_workItems.Values, Formatting.Indented);
        File.WriteAllText(FileName, serializedWorkItems);
    }

    private Dictionary<Guid, WorkItem> LoadWorkItemsFromFile()
    {
        if (File.Exists(FileName))
        {
            var serializedWorkItems = File.ReadAllText(FileName);
            return (JsonConvert.DeserializeObject<List<WorkItem>>(serializedWorkItems) ?? throw new InvalidOperationException())
                .ToDictionary(item => item.Id, item => item);
        }
        return new Dictionary<Guid, WorkItem>();
    }
}