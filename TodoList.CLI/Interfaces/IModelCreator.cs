namespace TodoList.CLI;

public interface IModelCreator<TModel>
{
    TModel Create();
}