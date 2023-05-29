namespace TodoList.CLI;

public interface IModelCreator<T>
{
    T Create();
}