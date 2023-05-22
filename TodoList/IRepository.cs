namespace TodoList;

public interface IRepository
{
    void Save<T>(string filePath, T value);
    Dictionary<TKey, TValue> Load<TKey, TValue>(string filePath);
}