namespace TodoList;

public abstract class BaseManager<TModel> where TModel : BaseModel
{
    public BaseManager(BaseRepository repository)
    {
        if (repository == null)
        {
            throw new ArgumentNullException(nameof(repository), "Не может иметь пустое значение! ");
        }

        this.repository = repository;
        models = GetModels();
        ModelAdded += OnAdded;
        ModelRemoved += OnRemoved;
        ModelNotFound += OnNotFound;
        ChangeDataUpdate += OnDataChangedUpdated;
    }

    protected event Action<TModel> ModelAdded;
    protected event Action<int> ModelRemoved;
    protected event Action<int> ModelNotFound;
    protected event Action<TModel, string> ChangeDataUpdate;
    protected Dictionary<int, TModel> models;
    protected BaseCreator<TModel> creator;

    protected BaseRepository repository;

    public virtual void Add()
    {
        TModel model = creator.Create();

        var modelIndex = models.Keys.Count + 1;

        models.Add(modelIndex, model);

        repository.Save(models);

        ModelAdded?.Invoke(model);
    }

    public virtual void Remove(int index)
    {
        if (models.ContainsKey(index))
        {
            models.Remove(index);

            var updatedModels = new Dictionary<int, TModel>();
            int newIndex = 1;

            foreach (KeyValuePair<int, TModel> kvp in models)
            {
                if (kvp.Key != index)
                {
                    updatedModels.Add(newIndex, kvp.Value);
                    newIndex++;
                }
            }

            models = updatedModels;

            repository.Save(models);

            ModelRemoved?.Invoke(index);
        }
        else
        {
            ModelNotFound?.Invoke(index);
        }
    }

    public virtual void Edit(int id, string title)
    {
        ChangeData(id, "обновлен");
        repository.Save(models);
    }

    protected void ChangeData(int id, string actionInfo)
    {
        if (models.ContainsKey(id) == false)
        {
            ModelNotFound?.Invoke(id);
        }

        if (models.TryGetValue(id, out TModel? existingModel))
        {
            if (existingModel is TModel existingIssue)
            {
                ChangeDataUpdate?.Invoke(existingIssue, actionInfo);
            }
            else
            {
                System.Console.WriteLine($"{typeof(TModel).Name} {id} не является типом {nameof(IssueModel)} и не может быть {actionInfo}! ");
            }
        }
    }

    private Dictionary<int, TModel> GetModels()
    {
        return repository.Load<int, TModel>();
    }

    public void Print()
    {
        foreach (var issue in models.OrderBy(x => x.Key))
        {
            Console.Write($"{issue.Key}) {issue.Value}\n");
        }
    }

    protected virtual void OnAdded(TModel model)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"{typeof(TModel).Name} добавлен: {model.Title}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    protected virtual void OnRemoved(int id)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"{typeof(TModel).Name} удален: {id}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    protected virtual void OnNotFound(int id)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine($"{typeof(TModel).Name} не найден(а): {id}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    protected virtual void OnDataChangedUpdated(TModel model, string updateInfo)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"{typeof(TModel).Name} {model.UniqueId}: {updateInfo}: ");
        Console.ForegroundColor = ConsoleColor.White;
    }
}