using TodoList;
using System;
using TodoList.CLI;

BaseRepository jsonRepository = new JsonRepository("data.json");

var issueCreator = new IssueConsoleCreator();
var groupCreator = new GroupConsoleCreator();

var issueManager = new IssueManager(jsonRepository, issueCreator);

var groupManager = new GroupManager(jsonRepository, groupCreator);

bool isContinue = true;

int index = 0;

while (isContinue == true)
{
    PrintMenu("группу");
    var input = Console.ReadKey();
    Console.WriteLine();
    switch (input.Key)
    {
        case MenuCommands.Add:
            groupManager.Add();
            break;
        case MenuCommands.Remove:
            index = InputAndGetIndex("группу", "удалить");
            groupManager.Remove(index);
            break;
        case MenuCommands.Edit:
            index = InputAndGetIndex("группу", "редактировать");
            groupManager.Edit(index, Console.ReadLine());
            break;
        case MenuCommands.IsDone:
            index = InputAndGetIndex("задачу", "отметить как выполненная");
            issueManager.ChangeIsDone(index);
            break;
        case MenuCommands.Exit:
            isContinue = false;
            if (isContinue)
            {
                Console.WriteLine("Нажмите на любую клавишу для выхода из приложения: ");
                Console.ReadLine();
                return;
            }
            break;
        case MenuCommands.Print:
            Console.WriteLine("Группы: ");
            groupManager.Print();
            System.Console.WriteLine("Задачи: ");
            issueManager.Print();
            Console.WriteLine();
            break;
        default:
            Console.WriteLine("Такой клавиши нету, попробуйте еще раз! ");
            Console.ReadKey();
            Console.Clear();
            break;
    }
}


int InputAndGetIndex(string title, string actionInfo)
{
    groupManager.Print();
    Console.Write($"Введите {title} который(ую) хотите {actionInfo}: ");
    var issueIndex = Convert.ToInt32(Console.ReadLine());
    return issueIndex;
}

void PrintMenu(string title)
{
    System.Console.WriteLine($"1 - Добавить {title}");
    System.Console.WriteLine($"2 - Удалить {title}");
    System.Console.WriteLine($"3 - Редактировать {title}");
    System.Console.WriteLine($"4 - Отметить {title} как выполненная");
    System.Console.WriteLine($"5 - Отсортиовать по значению");
    System.Console.WriteLine($"6 - Выйти");
    System.Console.WriteLine($"7 - Вывести {title}");
}

// void SortMenu()
// {
//     System.Console.WriteLine("1 - Отсортировать по названию");
//     System.Console.WriteLine("2 - Отсортировать по дате создания");
//     System.Console.WriteLine("3 - Отсортировать по Id");
//     System.Console.WriteLine("4 - Отсортировать по выполнению задачи");
// }