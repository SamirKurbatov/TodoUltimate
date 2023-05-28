using TodoList.CLI.Repositories;

namespace TodoList.CLI;

public interface IDataRepository<TModel>
{
    void Save(TodoData<TModel> value);
    TodoData<TModel> Load();
}