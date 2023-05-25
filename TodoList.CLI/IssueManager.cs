namespace TodoList;
public class IssueManager : BaseManager<IssueModel>
{
    public IssueManager(BaseRepository repository) : base(repository)
    {
        creator = new IssueConsoleCreator();
    }

    public void ChangeIsDone(int id)
    {
        ChangeData(id, "отмечена как выполнен(ая)");
        repository.Save(models);
    }
}