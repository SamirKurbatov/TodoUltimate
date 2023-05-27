using TodoList.CLI.Models;

namespace TodoList.CLI
{
    public class MainMenu
    {
        private readonly IssueManager issueManager;
        private readonly GroupManager groupManager;

        private int modelId;

        public MainMenu()
        {
            var issueRepository = new JsonRepository("issue.json");
            var groupRepository = new JsonRepository("group.json");

            var issueCreator = new BaseCreator<IssueModel>();
            var groupCreator = new BaseCreator<GroupModel>();

            issueManager = new IssueManager(issueRepository, issueCreator);
            groupManager = new GroupManager(groupRepository, groupCreator);
        }
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("Здравствуйте! Вас беспокоит Тудулист Курбатова.\n");

            bool isContinue = true;
            while (isContinue)
            {
                PrintMainMenu();
                var input = Console.ReadKey();
                Console.WriteLine();
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        ManageIssues();
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        ManageGroups();
                        break;
                    case ConsoleKey.D3:
                        issueManager.Add();
                        break;
                    case ConsoleKey.D4:
                        groupManager.Add();
                        break;
                    case ConsoleKey.D5:
                        Console.WriteLine("Нажмите любую клавишу для выхода из приложения:");
                        Console.ReadKey();
                        isContinue = false;
                        break;
                    default:
                        Console.WriteLine("Такой клавиши нет, попробуйте еще раз!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        private void PrintMainMenu()
        {
            Console.WriteLine($"1 - Просмотр задач");
            Console.WriteLine($"2 - Просмотр групп");
            Console.WriteLine($"3 - Добавить задачу");
            Console.WriteLine($"4 - Добавить группу");
            Console.WriteLine($"5 - Выйти");
        }

        private void ManageIssues()
        {
            bool isContinue = true;
            while (isContinue)
            {
                Console.Clear();
                PrintEditIssueMenu();
                issueManager.Print();
                var input = Console.ReadKey();
                Console.WriteLine();
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        issueManager.Print();
                        modelId = InputAndGetIndex("задачу", "редактировать");
                        var editStr = Console.ReadLine();
                        issueManager.Edit(modelId, string.IsNullOrWhiteSpace(editStr) == false ? editStr : "пустая строка");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D2:
                        issueManager.Print();
                        modelId = InputAndGetIndex("задачу", "удалить");
                        issueManager.Remove(modelId);
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D3:
                        issueManager.Print();
                        modelId = InputAndGetIndex("задачу", "отметить как выполненную");
                        issueManager.ChangeIsDone(modelId);
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D4:
                        isContinue = false;
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Такой клавиши нет, попробуйте еще раз!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ManageGroups()
        {
            bool isContinue = true;
            while (isContinue)
            {
                Console.Clear();
                PrintEditGroupMenu();
                groupManager.Print();
                var input = Console.ReadKey();
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        modelId = InputAndGetIndex("группу", "редактировать");
                        groupManager.Edit(modelId, Console.ReadLine());
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D2:
                        modelId = InputAndGetIndex("группу", "удалить");
                        groupManager.Remove(modelId);
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D3:
                        modelId = InputAndGetIndex("группу", "добавить задачу");
                        var issueData = issueManager.Models.Data;
                        var groupData = groupManager.Models.Data;
                        var groupModel = groupData.FirstOrDefault(x => x.Key == modelId).Value;
                        if (groupModel != null)
                        {
                            Console.Clear();
                            issueManager.Print();
                            modelId = InputAndGetIndex("задачу", "добавить");
                            var issueModel = issueData.FirstOrDefault(x => x.Key == modelId).Value;
                            if (issueModel != null)
                            {
                                groupManager.IssueAdded += groupManager.OnIssueAdded;
                                groupManager.AddIssueToGroup(issueModel, groupModel);
                            }
                        }
                        Console.ReadLine();
                        // ToDo добавление задачи в группу
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        isContinue = false;
                        break;
                    default:
                        Console.WriteLine("Такой клавиши нет, попробуйте еще раз!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private int InputAndGetIndex(string title, string actionInfo)
        {
            System.Console.WriteLine($"Выберите {title} которую хотите {actionInfo}");
            var issueIndex = Convert.ToInt32(Console.ReadLine());
            return issueIndex;
        }

        private void PrintEditIssueMenu()
        {
            Console.WriteLine($"1 - Редактировать задачи");
            Console.WriteLine($"2 - Удалить задачу");
            Console.WriteLine($"3 - Отметить задачу как выполненную");
            Console.WriteLine($"4 - Перейти к главному меню");
        }

        private void PrintEditGroupMenu()
        {
            Console.WriteLine($"1 - Редактировать группу");
            Console.WriteLine($"2 - Удалить группу");
            Console.WriteLine($"3 - Добавить в группу задачу");
            Console.WriteLine($"4 - Перейти к главному меню");
        }
    }
}