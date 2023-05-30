namespace TodoList.CLI;

public interface IFactoryModel<T>
{
    T Create();
}