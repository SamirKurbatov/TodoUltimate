using TodoList.CLI.Models;

namespace TodoList.CLI.Repositories
{
    [Serializable]
    public class TodoData<TData>
    {
        public Dictionary<int, TData> Data { get; set; }
        public TodoData()
        {
            Data = new Dictionary<int, TData>();
        }
    }
}