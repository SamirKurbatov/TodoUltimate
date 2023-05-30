using TodoList.CLI.BaseAbstractions;

namespace TodoList.CLI;
public class IssueController : BaseConsoleController<IssueModel>
{
    public IssueController(IDataRepository<IssueModel> repository, IModelFactory<IssueModel> creator) : base(repository, creator)
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