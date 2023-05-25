using TodoList.CLI.Models;

namespace TodoList.CLI;

public class GroupManager : BaseManager<GroupModel>
{
    public GroupManager(BaseRepository repository, BaseCreator<GroupModel> creator) : base(repository, creator) { }

    public GroupManager() { }
}