namespace TodoList.CLI;

public interface IModelFactory<T>
{
    T Create();
}