namespace TodoList
{
    public class IssueManager
    {
        protected static int idCounter = 0;

        private Dictionary<int, BaseModel> models;

        public IssueManager()
        {
            models = new Dictionary<int, BaseModel>();
        }

        public void Add()
        {
            BaseCreator creator = new IssueCreator();

            var issue = creator.Create();

            issue.Id = ++idCounter;
            models.Add(issue.Id, issue);

            System.Console.WriteLine("Задача добавлена! ");
        }

        public void Remove(int id)
        {
            if (models.ContainsKey(id) == false)
            {
                System.Console.WriteLine($"Задача {id} не найдена! ");
                return;
            }

            models.Remove(id);
            System.Console.WriteLine($"Задача {id} удалена! ");
        }

        public void Print()
        {
            foreach (var issue in models)
            {
                Console.WriteLine(issue.Key + ") " + issue.Value);
            }
        }
    }
}