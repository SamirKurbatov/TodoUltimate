namespace TodoList.CLI;
public class IssueManager : BaseManager<IssueModel>
{
    public IssueManager(IDataRepository<IssueModel> repository, IModelCreator<IssueModel> creator) : base(repository, creator)
    {
    }

    public void ChangeIsDone(int id)
    {
        ChangeData(id, data =>
        {
            data.IsCompleted = true;
            repository.Save(Models);
        }, "отмечена как выполнена");
    }
}