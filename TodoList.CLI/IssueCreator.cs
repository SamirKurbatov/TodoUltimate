namespace TodoList;

public class IssueCreator : BaseCreator<Issue>
{
    public override Issue Create()
    {
        Console.Write("Введите название задачи: ");
        string? title;
        do
        {
            title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.Write("Вы ничего не ввели попробуйте еще раз!\n");
            }
        } while (string.IsNullOrWhiteSpace(title));

        return new Issue(title);
    }
}