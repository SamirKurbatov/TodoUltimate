namespace TodoList;

public class IssueManager
{
    private int idCounter = 0;

    private Dictionary<int, BaseModel> models;

    private IssueCreator creator;

    private BaseModel issue;

    public IssueManager()
    {
        models = new Dictionary<int, BaseModel>();
        creator = new IssueCreator();
    }

    public void Add()
    {
        issue = creator.Create();

        issue.Id = idCounter + 1;
        idCounter++;

        models.Add(idCounter, issue);

        System.Console.WriteLine("Задача добавлена! ");
    }

    public void Remove(int id)
    {
        CheckExistsId(id);
        models.Remove(id);
        System.Console.WriteLine($"Задача {id} удалена! ");
    }

    public void Update(int id, string title)
    {
        CheckExistsId(id);

        if (models.TryGetValue(id, out BaseModel existingModel))
        {
            if (existingModel is Issue existingIssue)
            {
                existingIssue.Title = title;
                System.Console.WriteLine($"Задача {id} обновлена! ");
            }
            else
            {
                System.Console.WriteLine($"Задача {id} не является типом Issue и не может быть обновлена! ");
            }
        }
    }

    public void Print()
    {
        foreach (var issue in models.OrderBy(x => x.Key))
        {
            Console.WriteLine(issue.Value);
        }
    }

    private void CheckExistsId(int id)
    {
        if (models.ContainsKey(id) == false)
        {
            System.Console.WriteLine($"Задача {id} не найдена! ");
            return;
        }
    }
}