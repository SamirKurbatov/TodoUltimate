using TodoList.CLI;

public abstract class BaseCreator<TModel> : IModelCreator<TModel>
{
    public abstract TModel Create();
}