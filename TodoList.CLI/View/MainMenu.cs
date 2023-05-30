using TodoList.CLI.Models;
using TodoList.CLI.BaseAbstractions;

namespace TodoList.CLI
{
    /// <summary>
    /// Класс для работы с
    /// </summary>
    public class MainMenu
    {
        private readonly IssueController issueManager;
        private readonly GroupController groupManager;

        private int modelId;

        public MainMenu(IssueController issueManager, GroupController groupManager)
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
                        ManageItems(issueManager, "задачу");
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        ManageItems(groupManager, "группу");
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

        private void ManageItems<T>(BaseConsoleController<T> baseManager, string itemType) where T : BaseModel
        {
            while (true)
            {
                Console.Clear();
                PrintEditMenu(itemType);
                baseManager.PrintModels();
                var input = Console.ReadKey();
                Console.WriteLine();
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        EditItem(baseManager);
                        break;
                    case ConsoleKey.D2:
                        RemoveItem(baseManager);
                        break;
                    case ConsoleKey.D3:
                        if (itemType == "группу")
                        {
                            AddIssueToGroup();
                        }
                        else if (itemType == "задачу")
                        {
                            MarkTaskDone();
                        }
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Такой клавиши нет, попробуйте еще раз!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AddIssueToGroup()
        {
            var groupModel = GetModelFromUserChoice(groupManager.DataModels.Data, "группу", "добавить");

            if (groupModel != null)
            {
                Console.Clear();
                issueManager.PrintModels();
                var issueModel = GetModelFromUserChoice(issueManager.DataModels.Data, "задачи", "добавить");
                if (issueModel != null)
                {
                    groupManager.IssueAdded += groupManager.OnIssueAdded;
                    groupManager.AddIssueToGroup(issueModel, groupModel);
                }
            }
        }

        private void MarkTaskDone()
        {
            var issueModel = GetModelFromUserChoice(issueManager.DataModels.Data, "задачу", "отметить как выполненную");
            if (issueModel != null)
            {
                issueManager.ChangeIsDone(modelId);
            }
        }

        private void EditItem<T>(BaseConsoleController<T> baseManager) where T : BaseModel
        {
            if (baseManager.DataModels.Data.Count == 0)
            {
                Console.WriteLine("Пусто, редактировать нечего!");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            var modelId = GetUserChoice("редактировать");
            baseManager.Edit(modelId, Console.ReadLine());
            Console.ReadKey();
        }

        private void RemoveItem<T>(BaseConsoleController<T> baseManager)   where T : BaseModel
        {
            if (baseManager.DataModels.Data.Count == 0)
            {
                Console.WriteLine("Пусто, удалять нечего!");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            var modelId = GetUserChoice("удалить");
            baseManager.Remove(modelId);
            Console.ReadKey();
        }
        
        private int GetUserChoice(string actionInfo)
        {
            System.Console.WriteLine($"Выберите элемент который хотите {actionInfo}");
            var issueIndex = int.TryParse(Console.ReadLine(), out int index);
            return index;
        }

        private T? GetModelFromUserChoice<T>(Dictionary<int, T> models, string itemType, string actionInfo) where T : BaseModel
        {
            if (models.Values.Count == 0)
            {
                System.Console.WriteLine($"Пусто нельзя {actionInfo}");
                return default;
            }
            
            modelId = GetUserChoice(actionInfo);

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
            Console.WriteLine($"3 - {(menuType == "задачу" ? "Отметить" : "Добавить в")} {menuType} {(menuType == "задачу" ? "как выполненную" : "задачу")}");
            Console.WriteLine($"4 - Перейти к главному меню");
        }
    }
}