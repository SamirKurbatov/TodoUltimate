using TodoList.CLI;

public class BaseConsoleCreator<TModel> : IModelCreator<TModel>
{
    public virtual TModel Create()
    {
        Console.Write($"Введите {typeof(TModel).Name}: ");
        var title = CheckField($"{typeof(TModel).Name}");
        Type type = typeof(TModel);
        return (TModel)Activator.CreateInstance(type, title);
    }

    protected virtual string CheckField(string value)
    {
        string? project;
        do
        {
            project = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(project))
            {
                Console.WriteLine($"Вы не ввели значение {value} попробуйте еще раз!");
            }
        } while (string.IsNullOrWhiteSpace(project));
        return project;
    }
}