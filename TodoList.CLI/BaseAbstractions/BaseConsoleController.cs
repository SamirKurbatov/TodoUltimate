using TodoList.CLI.Repositories;

namespace TodoList.CLI.BaseAbstractions;

public class BaseConsoleController<T> : IManager<T> where T : BaseModel
{
    public BaseConsoleController(IDataRepository<T> repository, IModelFactory<T> baseFactory)
    {
        if (repository == null)
        {
            throw new ArgumentNullException(nameof(repository), "Не может иметь пустое значение! ");
        }

        this.repository = repository;
        modelFactory = baseFactory;
        DataModels = GetModels();
        SubscribeEvents();
    }

    public event Action<T>? ModelAdded;
    public event Action<int>? ModelRemoved;
    public event Action<int>? ModelNotFound;
    public event Action<T, string>? ChangeDataUpdate;
    protected IDataRepository<T> repository;
    protected IModelFactory<T> modelFactory;
    public TodoData<T> DataModels { get; private set; }

    public virtual void Add()
    {
        T model = modelFactory.Create();

        var modelIndex = DataModels.Data.Keys.Count + 1;

        DataModels.Data.Add(modelIndex, model);

        repository.SaveData(DataModels);

        ModelAdded?.Invoke(model);
    }

    public virtual void Remove(int index)
    {
        if (DataModels.Data.ContainsKey(index))
        {
            DataModels.Data.Remove(index);

            var updatedModels = new TodoData<T>();
            int newIndex = 1;

            foreach (KeyValuePair<int, T> kvp in DataModels.Data)
            {
                if (kvp.Key != index)
                {
                    updatedModels.Data.Add(newIndex, kvp.Value);
                    newIndex++;
                }
            }

            DataModels = updatedModels;

            repository.SaveData(DataModels);

            ModelRemoved?.Invoke(index);
        }
        else
        {
            ModelNotFound?.Invoke(index);
        }
    }

    private void SubscribeEvents()
    {
        ModelAdded += OnAdded;
        ModelRemoved += OnRemoved;
        ModelNotFound += OnNotFound;
        ChangeDataUpdate += OnDataChangedUpdated;
    }

    public virtual void Edit(int id, string changedTitle)
    {
        ChangeData(id, data =>
        {
            data.Title = changedTitle;
            repository.SaveData(DataModels);
        }, "обновлен");
    }

    protected void ChangeData(int id, Action<T> action, string actionInfo)
    {
        if (DataModels.Data.TryGetValue(id, out T? existingBaseModel))
        {
            action?.Invoke(existingBaseModel);
            ChangeDataUpdate?.Invoke(existingBaseModel, actionInfo);
        }
        else
        {
            ModelNotFound?.Invoke(id);
        }
    }

    public TodoData<T> GetModels()
    {
        return repository.LoadData();
    }

    public void PrintModels()
    {
        foreach (var model in DataModels.Data.OrderBy(x => x.Key))
        {
            Console.WriteLine($"{model.Key}) {model.Value}\n");
        }
    }

    protected virtual void OnAdded(T model)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"{typeof(T).Name} под названием {model.Title} добавлен. ");
        Console.ForegroundColor = ConsoleColor.White;
    }

    protected virtual void OnRemoved(int id)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"{typeof(T).Name} {id} удален. ");
        Console.ForegroundColor = ConsoleColor.White;
    }

    protected virtual void OnNotFound(int id)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine($"{typeof(T).Name} {id} не найден(а): ");
        Console.ForegroundColor = ConsoleColor.White;
    }

    protected virtual void OnDataChangedUpdated(T model, string updateInfo)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"{typeof(T).Name} {model.UniqueId} {updateInfo}.");
        Console.ForegroundColor = ConsoleColor.White;
    }
}