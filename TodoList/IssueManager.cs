namespace TodoList;

public class IssueManager : BaseManager<Issue>
{
    public IssueManager(IRepository repository, string path) : base(repository, path)
    {
        creator = new IssueCreator();
    }

    public override void Add()
    {
        base.Add();
    }
    public override void Remove(int id)
    {
        base.Remove(id);
    }

    public override void ChangeIsDone(int id)
    {
        base.ChangeIsDone(id);

        ChangeData(id, issue =>
        {
            issue.IsCompleted = true;
        }, "помечена как выполненная");
    }

    public override void Update(int id, string title)
    {
        base.Update(id, title);

        ChangeData(id, issue =>
        {
            issue.IsCompleted = true;
        }, "обновлена");
    }
}