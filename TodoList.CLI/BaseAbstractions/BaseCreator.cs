using TodoList.CLI;

public class BaseCreator<T> where T : BaseModel
{
    public virtual T Create()
    {
        Console.Write($"Введите {typeof(T).Name}: ");
        var title = CheckField($"{typeof(T).Name}");
        Type type = typeof(T);
        return (T)Activator.CreateInstance(type, title);
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