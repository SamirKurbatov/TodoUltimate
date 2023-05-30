using TodoList.CLI.Repositories;

namespace TodoList.CLI;

public abstract class BaseRepository<T> : IDataRepository<T>
{
    public string FileName { get; set; }
    protected BaseRepository(string fileName)
    {
        FileName = fileName;
    }
    public abstract void SaveData(TodoData<T> value);
    public abstract TodoData<T> LoadData();
}