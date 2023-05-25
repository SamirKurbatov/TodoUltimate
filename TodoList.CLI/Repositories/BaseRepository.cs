using TodoList.CLI.Repositories;

namespace TodoList;

public abstract class BaseRepository
{
    public string FileName { get; set; }
    protected BaseRepository(string fileName)
    {
        FileName = fileName;
    }
    public abstract void Save<T>(TodoData<T> value);
    public abstract TodoData<T> Load<T>();
}