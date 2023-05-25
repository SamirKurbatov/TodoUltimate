using TodoList.CLI.Models;

namespace TodoList.CLI
{
    public class GroupConsoleCreator : BaseCreator<GroupModel>
    {
        public override GroupModel Create()
        {
            Console.Write("Введите группу: ");
            var title = CheckField("группу");
            return new GroupModel(title);
        }
    }
}