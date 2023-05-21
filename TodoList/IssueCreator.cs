namespace TodoList
{
    public class IssueCreator : BaseCreator
    {
        public override BaseModel Create()
        {
            Console.Write("Введите название задачи: ");
            var title = Console.ReadLine() ?? "Вы ничего не ввели! ";
            return new Issue(title);
        }
    }
}