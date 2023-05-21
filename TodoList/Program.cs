using TodoList;

var issueManager = new IssueManager();

bool isContinue = true;

int index = 0;

while (isContinue == true)
{
    PrintMenu();
    var input = Console.ReadKey();
    System.Console.WriteLine();
    switch (input.Key)
    {
        case ConsoleKey.D1:
            issueManager.Add();
            break;
        case ConsoleKey.D2:
            index = GetIndex("задачу", "удалить");
            issueManager.Remove(index);
            break;
        case ConsoleKey.D3:
            index = GetIndex("задачу", "редактировать");
            issueManager.Update(index);
            break;
        case ConsoleKey.D4:
            break;
        case ConsoleKey.D5:
            break;
        case ConsoleKey.D6:
            isContinue = false;
            if (isContinue)
            {
                Console.WriteLine("Нажмите на любую клавишу для выхода из приложения: ");
                Console.ReadLine();
                return;
            }
            break;
        case ConsoleKey.D7:
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


int GetIndex(string title, string action)
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