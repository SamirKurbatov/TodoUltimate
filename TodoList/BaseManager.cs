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

        TaskAdded += OnTaskAdded;
        TaskRemoved += OnTaskRemoved;
        TaskNotFound += OnTaskNotFound;
    }
    protected event Action<TModel> TaskAdded;
    protected event Action<int> TaskRemoved;
    protected event Action<int> TaskNotFound;
    protected event Action<TModel, string> ChangeDataUpdate;

    private Dictionary<int, TModel> models;

    protected BaseCreator<TModel> creator;

    private IRepository repository;

    protected int idCounter = 0;

    protected readonly string PATH;

    public virtual void Add()
    {
        TModel model = creator.Create();

        model.Id = idCounter + 1;
        idCounter++;

        models.Add(idCounter, model);

        repository.Save(PATH, models);

        TaskAdded?.Invoke(model);
    }

    public virtual void Remove(int id)
    {
        models.Remove(id);

        repository.Save(PATH, models);

        TaskRemoved?.Invoke(id);
    }

    public abstract void Update(int id, string title);
    public abstract void ChangeIsDone(int id);

    protected void ChangeData(int id, Action<TModel, string> updateAction, string actionInfo)
    {
        CheckExistsId(id);

        if (models.TryGetValue(id, out TModel? existingModel))
        {
            if (existingModel is TModel existingIssue)
            {
                updateAction?.Invoke(existingIssue, actionInfo);
            }
            else
            {
                System.Console.WriteLine($"{typeof(TModel).Name} {id} не является типом {nameof(Issue)} и не может быть {actionInfo}! ");
            }
        }
    }

    private Dictionary<int, TModel> GetModels()
    {
        return repository.Load<int, TModel>(PATH);
    }

    private void CheckExistsId(int id)
    {
        if (models.ContainsKey(id) == false)
        {
            TaskNotFound?.Invoke(id);
        }
    }

    public void Print()
    {
        foreach (var issue in models.OrderBy(x => x.Key))
        {
            Console.WriteLine(issue.Value);
        }
    }

    public abstract void OnTaskAdded(TModel issue);

    public abstract void OnTaskRemoved(int id);

    public abstract void OnTaskNotFound(int id);

    public abstract void OnDataChangedUpdated(TModel model, string updateInfo);
}