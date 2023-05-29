using TodoList.CLI.Repositories;

namespace TodoList.CLI;

public class BaseConsoleManager<TModel> : IManager<TModel> where TModel : BaseModel
{
    public BaseConsoleManager(IDataRepository<TModel> repository, IModelCreator<TModel> baseCreator)
    {
        if (repository == null)
        {
            throw new ArgumentNullException(nameof(repository), "Не может иметь пустое значение! ");
        }

        this.repository = repository;
        Creator = baseCreator;
        DataModels = GetModels();
        SubscribeEvents();
    }

    public event Action<TModel>? ModelAdded;
    public event Action<int>? ModelRemoved;
    public event Action<int>? ModelNotFound;
    public event Action<TModel, string>? ChangeDataUpdate;
    protected IDataRepository<TModel> repository;
    public IModelCreator<TModel> Creator { get; set; }
    public TodoData<TModel> DataModels { get; private set; }

    public virtual void Add()
    {
        TModel model = Creator.Create();

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

            var updatedModels = new TodoData<TModel>();
            int newIndex = 1;

            foreach (KeyValuePair<int, TModel> kvp in DataModels.Data)
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

    protected void ChangeData(int id, Action<TModel> action, string actionInfo)
    {
        if (DataModels.Data.TryGetValue(id, out TModel? existingBaseModel))
        {
            action?.Invoke(existingBaseModel);
            ChangeDataUpdate?.Invoke(existingBaseModel, actionInfo);
        }
        else
        {
            ModelNotFound?.Invoke(id);
        }
    }

    public TodoData<TModel> GetModels()
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

    protected virtual void OnAdded(TModel model)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"{typeof(TModel).Name} под названием {model.Title} добавлен. ");
        Console.ForegroundColor = ConsoleColor.White;
    }

    protected virtual void OnRemoved(int id)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"{typeof(TModel).Name} {id} удален. ");
        Console.ForegroundColor = ConsoleColor.White;
    }

    protected virtual void OnNotFound(int id)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine($"{typeof(TModel).Name} {id} не найден(а): ");
        Console.ForegroundColor = ConsoleColor.White;
    }

    protected virtual void OnDataChangedUpdated(TModel model, string updateInfo)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine($"{typeof(TModel).Name} {model.UniqueId} {updateInfo}.");
        Console.ForegroundColor = ConsoleColor.White;
    }

    
}