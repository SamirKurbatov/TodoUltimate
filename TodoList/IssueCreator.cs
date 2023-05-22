namespace TodoList
{
    public class IssueCreator : BaseCreator<Issue>
    {
        public override Issue Create()
        {
            Console.Write("Введите название задачи: ");
            var title = Console.ReadLine() ?? "Вы ничего не ввели! ";
            return new Issue(title);
        }
    }
}