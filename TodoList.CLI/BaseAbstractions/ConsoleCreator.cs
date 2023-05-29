using TodoList.CLI;

public class ConsoleCreator<TModel> : BaseCreator<TModel>
{
    public override TModel Create()
    {
        Console.Write($"Введите {typeof(TModel).Name}");
        var title = Console.ReadLine();
        Type type = typeof(TModel);
        return (TModel)Activator.CreateInstance(type, title);
    }

    public string CheckField(string value)
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