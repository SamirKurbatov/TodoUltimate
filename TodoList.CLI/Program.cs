using TodoList;
using System;
using TodoList.CLI;


/// <summary>
/// Задачи в следующий раз:
/// </summary>
/// <returns></returns>

BaseRepository jsonRepository = new JsonRepository("data.json");

var issueCreator = new IssueConsoleCreator();
var groupCreator = new GroupConsoleCreator();

var issueManager = new IssueManager(jsonRepository, issueCreator);

var groupManager = new GroupManager(jsonRepository, groupCreator);



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
