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
    }

    protected event Action<TModel> ModelAdded;
    protected event Action<int> ModelRemoved;
    protected event Action<int> ModelNotFound;
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

    public abstract void Edit(int id, string title);
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
        return repository.Load<int, TModel>();
    }

    private void CheckExistsId(int id)
    {
        if (models.ContainsKey(id) == false)
        {
            ModelNotFound?.Invoke(id);
        }
    }

    public void Print()
    {
        foreach (var issue in models.OrderBy(x => x.Key))
        {
            Console.Write($"{issue.Key}) {issue.Value}\n");
        }
    }

    protected abstract void OnAdded(TModel model);

    protected abstract void OnRemoved(int id);

    protected abstract void OnNotFound(int id);

    protected abstract void OnDataChangedUpdated(TModel model, string updateInfo);
}