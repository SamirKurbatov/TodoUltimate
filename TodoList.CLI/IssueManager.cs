namespace TodoList;
public class IssueManager : BaseManager<IssueModel>
{
    public IssueManager(BaseRepository repository) : base(repository)
    {
        creator = new IssueConsoleCreator();
    }

    public void ChangeIsDone(int id)
    {
        ChangeData(id, data =>
        {
            data.IsCompleted = true;
            repository.Save(models);
        }, "отмечена как выполнена");
    }
}