using TodoList.CLI.Repositories;

namespace TodoList.CLI;

public interface IDataRepository<TModel>
{
    void SaveData(TodoData<TModel> value);
    TodoData<TModel> LoadData();
}