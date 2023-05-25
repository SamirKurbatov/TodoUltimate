using TodoList.CLI.Models;

namespace TodoList.CLI
{
    public abstract class MainMenu<T> where T : BaseModel
    {
        protected BaseManager<T> baseManager;
        public MainMenu()
        {
            baseManager = new BaseManager<T>();
        }

        public void PrintMenu(string title)
        {
            Console.Write("Здравствуйте вас приветствует тудулист Курбатова! ");

            bool isContinue = true;

            int index = 0;

            while (isContinue == true)
            {
                PrintMenu();
                var input = Console.ReadKey();
                Console.WriteLine();
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        break;
                    case ConsoleKey.D2:
                        break;
                    case ConsoleKey.D3:
                        break;
                    case ConsoleKey.D4:
                        break;
                    case ConsoleKey.D5:
                        break;
                    case ConsoleKey.D6:
                        break;
                    case ConsoleKey.D7:
                        break;
                    default:
                        Console.WriteLine("Такой клавиши нету, попробуйте еще раз! ");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        private void PrintMenu()
        {
            System.Console.WriteLine($"1 - Просмотр групп");
            System.Console.WriteLine($"2 - Просмотр задач");
            System.Console.WriteLine($"3 - Добавить задачу");
            System.Console.WriteLine($"4 - Добавить группу");
            System.Console.WriteLine($"5 - Управление группами");
            System.Console.WriteLine($"6 - Управление задачами");
            System.Console.WriteLine($"7 - Выйти");
        }

        protected abstract void PrintEntities();
        protected abstract void EditMenu(string title);
        protected abstract void PrintEditMenu();
    }

    public class GroupMenu : MainMenu<GroupModel>
    {
        protected override void PrintEntities()
        {
            Console.WriteLine("Группы: ");
            baseManager.Print();
        }

        protected override void EditMenu(string title)
        {
            bool isContinue = true;

            int index = 0;

            while (isContinue == true)
            {
                var input = Console.ReadKey();
                Console.WriteLine();
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        baseManager.Add();
                        break;
                    case ConsoleKey.D2:
                        index = baseManager.InputAndGetIndex("задачу", "удалить");
                        baseManager.Remove(index);
                        break;
                    case ConsoleKey.D3:
                        index = baseManager.InputAndGetIndex("задачу", "редактировать");
                        baseManager.Edit(index, Console.ReadLine());
                        break;
                    case ConsoleKey.D4:
                        isContinue = false;
                        if (isContinue)
                        {
                            Console.WriteLine("Нажмите на любую клавишу для выхода из приложения: ");
                            Console.ReadLine();
                            return;
                        }
                        break;
                    case ConsoleKey.D5:
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Такой клавиши нету, попробуйте еще раз! ");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        protected override void PrintEditMenu()
        {
            System.Console.WriteLine($"1 - Редактировать задачу");
            System.Console.WriteLine($"2 - Удалить задачу");
            System.Console.WriteLine($"3 - Отметить задачу как выполненная");
            System.Console.WriteLine($"4 - Перейти к главному меню");
            System.Console.WriteLine($"5 - Выйти");
        }
    }

    public class IssueMenu : MainMenu<IssueModel>
    {
        protected override void PrintEntities()
        {
            Console.WriteLine("Задачи: ");
            baseManager.Print();
        }

        protected override void EditMenu(string title)
        {
            bool isContinue = true;

            int index = 0;

            while (isContinue == true)
            {
                var input = Console.ReadKey();
                Console.WriteLine();
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        baseManager.Add();
                        break;
                    case ConsoleKey.D2:
                        index = index = baseManager.InputAndGetIndex("задачу", "удалить");
                        baseManager.Remove(index);
                        break;
                    case ConsoleKey.D3:
                        index = index = baseManager.InputAndGetIndex("задачу", "удалить");
                        baseManager.Edit(index, Console.ReadLine());
                        break;
                    case ConsoleKey.D4:
                        isContinue = false;
                        if (isContinue)
                        {
                            Console.WriteLine("Нажмите на любую клавишу для выхода из приложения: ");
                            Console.ReadLine();
                            return;
                        }
                        break;
                    case ConsoleKey.D5:
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Такой клавиши нету, попробуйте еще раз! ");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        protected override void PrintEditMenu()
        {
            System.Console.WriteLine($"1 - Редактировать задачу");
            System.Console.WriteLine($"2 - Удалить задачу");
            System.Console.WriteLine($"3 - Отметить задачу как выполненная");
            System.Console.WriteLine($"4 - Перейти к главному меню");
            System.Console.WriteLine($"5 - Выйти");
        }
    }
}