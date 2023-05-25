namespace TodoList.CLI;
public class IssueManager : BaseManager<IssueModel>
{
    public IssueManager(BaseRepository repository, BaseCreator<IssueModel> creator) : base(repository, creator)
    {
    }

    public IssueManager()
    {
        
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