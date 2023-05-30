using TodoList.CLI.Models;

namespace TodoList.CLI.Interfaces;

public interface IGroupController<TModel, TGroup> where TGroup : BaseGroupModel<TModel>
{
    void AddModelToGroup(TModel model, TGroup group);
}