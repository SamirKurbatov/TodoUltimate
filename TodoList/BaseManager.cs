namespace TodoList;

public abstract class BaseManager<TModel> where TModel : BaseModel
{
    public BaseManager(IRepository repository, string path)
    {
        if (repository == null)
        {
            throw new ArgumentNullException(nameof(repository), "Не может иметь пустое значение! ");
        }

        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentNullException(nameof(path), "Не может быть иметь пустое начение! ");
        }

        this.repository = repository;
        PATH = path;
        models = GetModels();
    }
    protected int idCounter = 0;

    protected readonly string PATH;

    private IRepository repository;

    private Dictionary<int, TModel> models;

    protected BaseCreator<TModel> creator;

    public virtual void Add()
    {
        TModel model = creator.Create();

        model.Id = idCounter + 1;
        idCounter++;

        models.Add(idCounter, model);

        repository.Save(PATH, models);

        System.Console.WriteLine($"{typeof(TModel).Name} добавлен(а)! ");
    }

    public virtual void Remove(int id)
    {
        models.Remove(id);
        repository.Save(PATH, models);
    }

    public abstract void Update(int id, string title);
    public abstract void ChangeIsDone(int id);

    protected void ChangeData(int id, Action<TModel> updateAction, string action)
    {
        CheckExistsId(id, action);

        if (models.TryGetValue(id, out TModel? existingModel))
        {
            if (existingModel is TModel existingIssue)
            {
                updateAction(existingIssue);
            }
            else
            {
                System.Console.WriteLine($"{typeof(TModel).Name} {id} не является типом {nameof(Issue)} и не может быть {action}! ");
            }
        }
    }

    private Dictionary<int, TModel> GetModels()
    {
        return repository.Load<int, TModel>(PATH);
    }

    private void CheckExistsId(int id, string action)
    {
        if (models.ContainsKey(id) == false)
        {
            System.Console.WriteLine($"{typeof(TModel).Name} {id} не найден(а)! ");
        }
        else
        {
            System.Console.WriteLine($"{typeof(TModel).Name} {id} {action}! ");
        }
    }

    public void Print()
    {
        foreach (var issue in models.OrderBy(x => x.Key))
        {
            Console.WriteLine(issue.Value);
        }
    }

}