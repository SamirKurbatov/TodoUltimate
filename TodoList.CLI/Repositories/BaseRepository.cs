namespace TodoList;

public abstract class BaseRepository
{
    protected readonly string FilePath;
    protected BaseRepository(string filePath)
    {
        FilePath = filePath;
    }
    public abstract void Save<T>(T value);
    public abstract Dictionary<TKey, TValue> Load<TKey, TValue>();
}