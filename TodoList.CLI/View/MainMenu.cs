using TodoList.CLI.Models;

namespace TodoList.CLI
{
    public class MainMenu
    {
        private readonly IssueManager issueManager;
        private readonly GroupManager groupManager;

        private int modelId;

        public MainMenu(IssueManager issueManager, GroupManager groupManager)
        {
            this.issueManager = issueManager;
            this.groupManager = groupManager;
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
                        ManageItems(issueManager, "задачи");
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        ManageItems(groupManager, "группы");
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

        private void ManageItems<T>(BaseManager<T> manager, string itemType) where T : BaseModel
        {
            bool isContinue = true;
            while (isContinue)
            {
                Console.Clear();
                PrintEditMenu(itemType);
                manager.Print();
                var input = Console.ReadKey();
                Console.WriteLine();

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        manager.Print();
                        modelId = GetUserChoice(itemType, "редактировать");
                        var editStr = Console.ReadLine();
                        manager.Edit(modelId, "редактировать");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D2:
                        manager.Print();
                        modelId = GetUserChoice(itemType, "удалить");
                        manager.Remove(modelId);
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D3:
                        if (itemType == "группы")
                        {
                            var groupModel = GetModelFromUserChoice(groupManager.Models.Data, itemType, "добавить");
                            if (groupModel != null)
                            {
                                Console.Clear();
                                issueManager.Print();
                                var issueModel = GetModelFromUserChoice(issueManager.Models.Data, "задачи", "добавить");
                                if (issueModel != null)
                                {
                                    groupManager.IssueAdded += groupManager.OnIssueAdded;
                                    groupManager.AddIssueToGroup(issueModel, groupModel);
                                }
                            }
                        }
                        else if (itemType == "задачи")
                        {
                            var issueModel = GetModelFromUserChoice(issueManager.Models.Data, itemType, "редактировать");
                            if (issueModel != null)
                            {
                                issueManager.ChangeIsDone(modelId);
                            }
                        }
                        Console.ReadLine();
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

        private int GetUserChoice(string title, string actionInfo)
        {
            System.Console.WriteLine($"Выберите {title} которую хотите {actionInfo}");
            var issueIndex = Convert.ToInt32(Console.ReadLine());
            return issueIndex;
        }

        private T GetModelFromUserChoice<T>(Dictionary<int, T> models, string itemType, string actionInfo) where T : BaseModel
        {
            modelId = GetUserChoice(itemType, actionInfo);

            if (models.TryGetValue(modelId, out T? model))
            {
                return model;
            }

            Console.WriteLine($"Не удалось найти выбранную {itemType}. Повторите попытку.");

            Console.ReadKey();

            return default;
        }


        private void PrintEditMenu(string menuType)
        {
            Console.WriteLine($"1 - Редактировать {menuType}");
            Console.WriteLine($"2 - Удалить {menuType}");
            Console.WriteLine($"3 - {(menuType == "задачи" ? "Отметить" : "Добавить в")} {menuType} {(menuType == "задачи" ? "как выполненную" : "задачу")}");
            Console.WriteLine($"4 - Перейти к главному меню");
        }
    }
}