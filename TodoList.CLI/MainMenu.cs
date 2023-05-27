using TodoList.CLI.Models;

namespace TodoList.CLI
{
    public class MainMenu
    {
        private BaseManager<T> SetManager<T>(BaseRepository baseRepository, BaseCreator<T> baseCreator) where T : BaseModel
        {
            return new BaseManager<T>(baseRepository, baseCreator);
        }

        public void Start()
        {
            Console.WriteLine("Здравствуйте! Вас приветствует Тудулист Курбатова.\n");
            bool isContinue = true;
            var repository = new JsonRepository("data.json");

            var issueCreator = new BaseCreator<IssueModel>();
            var groupCreator = new BaseCreator<GroupModel>();

            var issueManager = SetManager(repository, issueCreator);
            var groupManager = SetManager(repository, groupCreator);
            while (isContinue)
            {
                PrintMainMenu();
                var input = Console.ReadKey();
                Console.WriteLine();
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        issueManager.Print();
                        break;
                    case ConsoleKey.D2:
                        groupManager.Print();
                        break;
                    case ConsoleKey.D3:
                        issueManager.Add();
                        break;
                    case ConsoleKey.D4:
                        groupManager.Add();
                        break;
                    case ConsoleKey.D5:
                        break;
                    case ConsoleKey.D6:
                        break;
                    case ConsoleKey.D7:
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
            Console.WriteLine($"5 - Управление задачами");
            Console.WriteLine($"6 - Управление группами");
            Console.WriteLine($"7 - Выйти");
        }


        // protected virtual void EditMenu(string title)
        // {
        //     bool isContinue = true;
        //     int index = 0;

        //     while (isContinue)
        //     {
        //         var input = Console.ReadKey();
        //         Console.WriteLine();
        //         switch (input.Key)
        //         {
        //             case ConsoleKey.D1:
        //                 baseManager.Add();
        //                 break;
        //             case ConsoleKey.D2:
        //                 index = baseManager.InputAndGetIndex($"{title}", "удалить");
        //                 baseManager.Remove(index);
        //                 break;
        //             case ConsoleKey.D3:
        //                 index = baseManager.InputAndGetIndex($"{title}", "редактировать");
        //                 baseManager.Edit(index, Console.ReadLine());
        //                 break;
        //             case ConsoleKey.D4:
        //                 isContinue = false;
        //                 if (isContinue)
        //                 {
        //                     Console.WriteLine("Нажмите любую клавишу для выхода из приложения:");
        //                     Console.ReadLine();
        //                     return;
        //                 }
        //                 break;
        //             case ConsoleKey.D5:
        //                 Console.WriteLine();
        //                 break;
        //             default:
        //                 Console.WriteLine("Такой клавиши нет, попробуйте еще раз!");
        //                 Console.ReadKey();
        //                 Console.Clear();
        //                 break;
        //         }
        //     }
        // }

        // protected virtual void PrintEditMenu()
        // {
        //     Console.WriteLine($"1 - Редактировать {typeof(T).Name}");
        //     Console.WriteLine($"2 - Удалить {typeof(T).Name}");
        //     Console.WriteLine($"3 - Отметить {typeof(T).Name} как выполненную");
        //     Console.WriteLine($"4 - Перейти к главному меню");
        //     Console.WriteLine($"5 - Выйти");
        // }
    }
}
