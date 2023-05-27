public class BaseCreator<T> where T : BaseModel
{
    public virtual T Create()
    {
        Console.Write($"Введите {typeof(T).Name}: ");
        var title = CheckField($"{typeof(T).Name}");
        return (T)Activator.CreateInstance(typeof(T), title);
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