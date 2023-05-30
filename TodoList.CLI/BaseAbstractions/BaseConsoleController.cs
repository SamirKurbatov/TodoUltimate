using TodoList.CLI.Repositories;

namespace TodoList.CLI.BaseAbstractions;

public class BaseConsoleController<T> : IDataRepository<T> where T : BaseModel
{
    public event Action<T>? ModelAdded;
    public event Action<int>? ModelRemoved;
    public event Action<int>? ModelNotFound;
    public event Action<T, string>? ChangeDataUpdate;
    public BaseConsoleController(IDataRepository<T> repository, IFactoryModel<T> baseFactory)
    {
        if (repository == null)
        {
            throw new ArgumentNullException(nameof(repository), "Repository cannot be null! ");
        }

        Repository = repository;
        ModelFactory = baseFactory;
        DataModels = LoadData();
        SubscribeEvents();
    }

    public IDataRepository<T> Repository { get; set; }
    public IFactoryModel<T> ModelFactory { get; set; }

    public TodoData<T> DataModels { get; set; }

    public virtual void Add()
    {
        T model = ModelFactory.Create();

        var modelIndex = DataModels.Data.Keys.Count + 1;

        DataModels.Data.Add(modelIndex, model);

        SaveData(DataModels);

        ModelAdded?.Invoke(model);
    }

    public virtual void Remove(int index)
    {
        if (DataModels.Data.ContainsKey(index))
        {
            DataModels.Data.Remove(index);

            TodoData<T> updatedModels = GetUpdatedData(index);

            DataModels = updatedModels;

            SaveData(DataModels);

            ModelRemoved?.Invoke(index);
        }
        else
        {
            ModelNotFound?.Invoke(index);
        }
    }
    /// <summary>
    /// Метод требуется для создания словаря, который вскоре сможет обновлять ключи (int) после удаления задач
    /// </summary>
    /// <param name="index"></param>
    /// <returns>Обновленный словарь</returns>

    private TodoData<T> GetUpdatedData(int index)
    {
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

        return updatedModels;
    }

    public virtual void Edit(int id, string changedTitle)
    {
        ChangeData(id, data =>
        {
            data.Title = changedTitle;
            SaveData(DataModels);
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

    public void SaveData(TodoData<T> value)
    {
        Repository.SaveData(DataModels);
    }

    public TodoData<T> LoadData()
    {
        return Repository.LoadData();
    }

    public void PrintModels()
    {
        foreach (var model in DataModels.Data.OrderBy(x => x.Key))
        {
            Console.WriteLine($"{model.Key}) {model.Value}\n");
        }
    }

    private void SubscribeEvents()
    {
        ModelAdded += OnAdded;
        ModelRemoved += OnRemoved;
        ModelNotFound += OnNotFound;
        ChangeDataUpdate += OnDataChangedUpdated;
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