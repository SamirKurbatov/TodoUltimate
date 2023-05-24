using TodoList;

BaseRepository jsonRepository = new JsonRepository("data.json");

var issueManager = new IssueManager(jsonRepository);

bool isContinue = true;

int index = 0;

while (isContinue == true)
{
    PrintMenu();
    var input = Console.ReadKey();
    System.Console.WriteLine();
    switch (input.Key)
    {
        case MenuCommands.Add:
            issueManager.Add();
            break;
        case MenuCommands.Remove:
            index = InputAndGetIndex("задачу", "удалить");
            issueManager.Remove(index);
            break;
        case MenuCommands.Edit:
            index = InputAndGetIndex("задачу", "редактировать");
            issueManager.Edit(index, Console.ReadLine());
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
            System.Console.WriteLine("Задачи: ");
            issueManager.Print();
            System.Console.WriteLine();
            break;
        default:
            System.Console.WriteLine("Такой клавиши нету, попробуйте еще раз! ");
            Console.ReadKey();
            Console.Clear();
            break;
    }
}


int InputAndGetIndex(string title, string action)
{
    issueManager.Print();
    Console.Write($"Введите {title} который(ую) хотите {action}: ");
    var issueIndex = Convert.ToInt32(Console.ReadLine());
    return issueIndex;
}

void PrintMenu()
{
    System.Console.WriteLine("1 - Добавить задачу");
    System.Console.WriteLine("2 - Удалить задачу");
    System.Console.WriteLine("3 - Редактировать задачу");
    System.Console.WriteLine("4 - Отметить задачу как выполненная");
    System.Console.WriteLine("5 - Отсортиовать по значению");
    System.Console.WriteLine("6 - Выйти");
    System.Console.WriteLine("7 - Вывести задачи");
}

// void SortMenu()
// {
//     System.Console.WriteLine("1 - Отсортировать по названию");
//     System.Console.WriteLine("2 - Отсортировать по дате создания");
//     System.Console.WriteLine("3 - Отсортировать по Id");
//     System.Console.WriteLine("4 - Отсортировать по выполнению задачи");
// }