using TodoList.CLI;

public abstract class BaseCreator<TModel> : IModelCreator<TModel>
{
    public abstract TModel Create();
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