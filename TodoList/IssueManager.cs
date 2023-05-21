namespace TodoList
{
    public class IssueManager
    {
        private int idCounter = 0;

        private Dictionary<int, BaseModel> models;

        private BaseCreator creator;

        private BaseModel issue;

        public IssueManager()
        {
            models = new Dictionary<int, BaseModel>();
            creator = new IssueCreator();
        }

        public void Add()
        {
            issue = creator.Create();

            idCounter++;
            issue.Id = idCounter;

            models.Add(idCounter, issue);

            System.Console.WriteLine("Задача добавлена! ");
        }

        public void Remove(int id)
        {
            FindId(id);

            models.Remove(id);
            System.Console.WriteLine($"Задача {id} удалена! ");
        }

        public void Update(int id)
        {
            FindId(id);

            models.Select(x => x.Value.Id == id);
        }

        public void Print()
        {
            foreach (var issue in models.OrderBy(x => x.Key))
            {
                Console.WriteLine(issue.Value);
            }
        }

        private void FindId(int id)
        {
            if (models.ContainsKey(id) == false)
            {
                System.Console.WriteLine($"Задача {id} не найдена! ");
                return;
            }
        }
    }
}