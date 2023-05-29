using TodoList.CLI.Repositories;

namespace TodoList.CLI;

public interface IManager<TModel>
{
    void Add();
    void Remove(int id);
    void Edit(int id, string changedTitle);
    void PrintModels();
    TodoData<TModel> GetModels();
}
