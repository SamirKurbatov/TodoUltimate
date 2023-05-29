namespace TodoList.CLI;
public class IssueManager : BaseConsoleManager<IssueModel>
{
    public IssueManager(IDataRepository<IssueModel> repository, IModelCreator<IssueModel> creator) : base(repository, creator)
    {
    }

    public void ChangeIsDone(int id)
    {
        ChangeData(id, data =>
        {
            data.IsCompleted = true;
            repository.SaveData(DataModels);
        }, "отмечена как выполнена");
    }
}