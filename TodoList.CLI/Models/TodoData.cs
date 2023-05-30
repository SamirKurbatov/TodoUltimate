namespace TodoList.CLI.Repositories;

[Serializable]
public class TodoData<T>
{
    public Dictionary<int, T> Data { get; set; }
    public TodoData()
    {
        Data = new Dictionary<int, T>();
    }
}