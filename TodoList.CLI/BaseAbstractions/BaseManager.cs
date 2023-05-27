using TodoList.CLI.Repositories;

namespace TodoList.CLI;

public class BaseManager<TModel> where TModel : BaseModel
{
    public BaseManager(BaseRepository repository, BaseCreator<TModel> baseCreator)
    {
        if (repository == null)
        {
            throw new ArgumentNullException(nameof(repository), "Не может иметь пустое значение! ");
        }

        this.repository = repository;
        Creator = baseCreator;
        Models = GetModels();
        ModelAdded += OnAdded;
        ModelRemoved += OnRemoved;
        ModelNotFound += OnNotFound;
        ChangeDataUpdate += OnDataChangedUpdated;
    }

    public event Action<TModel> ModelAdded;
    public event Action<int> ModelRemoved;
    public event Action<int> ModelNotFound;
    public event Action<TModel, string> ChangeDataUpdate;
    public TodoData<TModel> Models { get; private set; }
    public BaseCreator<TModel> Creator { get; set; }
    protected BaseRepository repository;

    public virtual void Add()
    {
        TModel model = Creator.Create();

        var modelIndex = Models.Data.Keys.Count + 1;

        Models.Data.Add(modelIndex, model);

        repository.Save(Models);

        ModelAdded?.Invoke(model);
    }

    public virtual void Remove(int index)
    {
        if (Models.Data.ContainsKey(index))
        {
            Models.Data.Remove(index);

            var updatedModels = new TodoData<TModel>();
            int newIndex = 1;

            foreach (KeyValuePair<int, TModel> kvp in Models.Data)
            {
                if (kvp.Key != index)
                {
                    updatedModels.Data.Add(newIndex, kvp.Value);
                    newIndex++;
                }
            }

            Models = updatedModels;

            repository.Save(Models);

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
            repository.Save(Models);
        }, "обновлен");
    }

    protected void ChangeData(int id, Action<TModel> action, string actionInfo)
    {
        if (Models.Data.ContainsKey(id) == false)
        {
            ModelNotFound?.Invoke(id);
        }

        if (Models.Data.TryGetValue(id, out TModel? existingBaseModel))
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

    public TodoData<TModel> GetModels()
    {
        return repository.Load<TModel>();
    }

    public void Print()
    {
        foreach (var model in Models.Data.OrderBy(x => x.Key))
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