using TodoList.CLI.Repositories;

namespace TodoList.CLI;

public interface IDataRepository<T>
{
    void SaveData(TodoData<T> value);
    TodoData<T> LoadData();
}