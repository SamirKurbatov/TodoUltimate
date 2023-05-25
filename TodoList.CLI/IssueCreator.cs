namespace TodoList;

public class IssueCreator : BaseCreator<Issue>
{
    public override Issue Create()
    {
        Console.Write("Введите название задачи: ");
        var title = CheckField("задачи");
        Console.Write("Введите проект или область: ");
        var project = CheckField("проекта");
        return new Issue(title, project);
    }

    private static string CheckField(string value)
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