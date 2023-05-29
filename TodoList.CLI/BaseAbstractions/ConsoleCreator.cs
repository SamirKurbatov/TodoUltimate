using TodoList.CLI;

public class ConsoleCreator<T> : BaseCreator<T> where T : BaseModel, new()
{
    public override T Create()
    {
        var name = $"{typeof(T).Name}";
        Console.Write($"Введите название {name}: ");
        var title = CheckField(name);
        return new T { Title = title };
    }

    private string CheckField(string fieldName)
    {
        string? value;
        do
        {
            value = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine($"Вы не ввели значение {fieldName} попробуйте еще раз!");
            }
        } while (string.IsNullOrWhiteSpace(value));
        return value;
    }
}