using TodoList.CLI.Repositories;

namespace TodoList;

public abstract class BaseManager<TModel> where TModel : BaseModel
{
    public BaseManager(BaseRepository repository, BaseCreator<TModel> baseCreator)
    {
        if (repository == null)
        {
            throw new ArgumentNullException(nameof(repository), "Не может иметь пустое значение! ");
        }

        this.repository = repository;
        creator = baseCreator;
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
    protected TodoData<TModel> models;
    protected BaseCreator<TModel> creator;

    protected BaseRepository repository;

    public virtual void Add()
    {
        TModel model = creator.Create();

        var modelIndex = models.Data.Keys.Count + 1;

        models.Data.Add(modelIndex, model);

        repository.Save(models);

        ModelAdded?.Invoke(model);
    }

    public virtual void Remove(int index)
    {
        if (models.Data.ContainsKey(index))
        {
            models.Data.Remove(index);

            var updatedModels = new TodoData<TModel>();
            int newIndex = 1;

            foreach (KeyValuePair<int, TModel> kvp in models.Data)
            {
                if (kvp.Key != index)
                {
                    updatedModels.Data.Add(newIndex, kvp.Value);
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
        ChangeData(id, data =>
        {
            data.Title = title;
            repository.Save(models);
        }, "обновлен");
    }

    protected void ChangeData(int id, Action<TModel> action, string actionInfo)
    {
        if (models.Data.ContainsKey(id) == false)
        {
            ModelNotFound?.Invoke(id);
        }

        if (models.Data.TryGetValue(id, out TModel? existingBaseModel))
        {
            if (existingBaseModel is TModel existingModel)
            {
                action?.Invoke(existingModel);
                ChangeDataUpdate?.Invoke(existingModel, actionInfo);
            }
            else
            {
                System.Console.WriteLine($"{typeof(TModel).Name} {id} не может быть {actionInfo}! ");
            }
        }
    }

    private TodoData<TModel> GetModels()
    {
        return repository.Load<TModel>();
    }

    public void Print()
    {
        foreach (var model in models.Data.OrderBy(x => x.Key))
        {
            Console.Write($"{model.Key}) {model.Value}\n");
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
        System.Console.WriteLine($"{typeof(TModel).Name} {model.UniqueId}: {updateInfo}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}