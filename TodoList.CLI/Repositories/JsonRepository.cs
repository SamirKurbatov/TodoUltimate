using System.Text.Json;
using TodoList.CLI.Repositories;

namespace TodoList.CLI;

public class JsonRepository<TModel> : BaseRepository<TModel>
{
    private readonly string dataFolderPath = AppDomain.CurrentDomain.BaseDirectory;
    private string jsonFilePath;

    public JsonRepository(string fileName) : base(fileName)
    {
        jsonFilePath = Path.Combine(dataFolderPath, FileName);
    }

    public override TodoData<TModel> Load()
    {
         try
        {
            if (File.Exists(jsonFilePath) == true)
            {
                using (var fs = new FileStream(jsonFilePath, FileMode.OpenOrCreate))
                {
                    if (JsonSerializer.Deserialize<TodoData<TModel>>(fs) is TodoData<TModel> items)
                    {
                        return items;
                    }
                    else
                    {
                        return new TodoData<TModel>();
                    }
                }
            }
            else
            {
                return new TodoData<TModel>();
            }
        }
        catch (IOException ex)
        {
            System.Console.WriteLine($"Error deserialize file {jsonFilePath}: {ex.Message}");
            return new TodoData<TModel>();
        }
    }

    public override void Save(TodoData<TModel> value)
    {
        if (Directory.Exists(dataFolderPath) == false)
        {
            Directory.CreateDirectory(dataFolderPath);
        }

        var options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        string jsonString = JsonSerializer.Serialize(value, options);
        File.WriteAllText(jsonFilePath, jsonString);
    }
}
