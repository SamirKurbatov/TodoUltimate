using TodoList.CLI.Models;
using TodoList.CLI.BaseAbstractions;

namespace TodoList.CLI;

public class MainMenu
{
    private readonly IssueController issueController;
    private readonly IssueGroupController groupController;

    private int modelId;

    public MainMenu(IssueController issueManager, IssueGroupController groupManager)
    {
        this.issueController = issueManager;
        this.groupController = groupManager;
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
                    ManageItems(issueController, "задачу");
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    ManageItems(groupController, "группу");
                    break;
                case ConsoleKey.D3:
                    issueController.Add();
                    break;
                case ConsoleKey.D4:
                    groupController.Add();
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
                        MarkIssueDone();
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
        var groupModel = GetModelFromUserChoice(groupController.DataModels.Data, "группу", "добавить");

        if (groupModel != null)
        {
            Console.Clear();
            issueController.PrintModels();
            var issueModel = GetModelFromUserChoice(issueController.DataModels.Data, "задачи", "добавить");
            if (issueModel != null)
            {
                // groupController.IssueAdded += groupController.OnIssueAdded;
                groupController.AddModelToGroup(issueModel, groupModel);
            }
        }
    }

    private void MarkIssueDone()
    {
        var issueModel = GetModelFromUserChoice(issueController.DataModels.Data, "задачу", "отметить как выполненную");
        if (issueModel != null)
        {
            issueController.ChangeIsDone(modelId);
        }
    }

    private void EditItem<T>(BaseConsoleController<T> baseController) where T : BaseModel
    {
        string removeAction = "редактировать";
        if (IsAvailableInData(baseController, removeAction) == false)
        {
            return;
        }
        
        var modelId = GetUserChoice(removeAction);
        baseController.Edit(modelId, Console.ReadLine());
        Console.ReadKey();
    }

    private void RemoveItem<T>(BaseConsoleController<T> baseController) where T : BaseModel
    {
        string removeAction = "удалять";
        if (IsAvailableInData(baseController, removeAction) == false)
        {
            return;
        }

        var modelId = GetUserChoice(removeAction);
        baseController.Remove(modelId);
        Console.ReadKey();
    }
    private bool IsAvailableInData<T>(BaseConsoleController<T> baseController, string actionInfo) where T : BaseModel
    {
        if (baseController.DataModels.Data.Count == 0)
        {
            Console.WriteLine($"Пусто, {actionInfo} нечего!");
            Console.ReadKey();
            Console.Clear();
            return false;
        }
        return true;
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