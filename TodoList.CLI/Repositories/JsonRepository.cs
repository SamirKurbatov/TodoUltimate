using System.Text.Json;
using TodoList.CLI.Repositories;

namespace TodoList.CLI;

public class JsonRepository : BaseRepository
{
    private readonly string dataFolderPath = "C:/Users/user/Desktop/super-fortnight/TodoList.CLI/Data";

    private string jsonFilePath;

    public JsonRepository(string fileName) : base(fileName)
    {
        jsonFilePath = Path.Combine(dataFolderPath, FileName);
    }

    public override TodoData<T> Load<T>()
    {
         try
        {
            if (File.Exists(jsonFilePath) == true)
            {
                using (var fs = new FileStream(jsonFilePath, FileMode.OpenOrCreate))
                {
                    if (JsonSerializer.Deserialize<TodoData<T>>(fs) is TodoData<T> items)
                    {
                        return items;
                    }
                    else
                    {
                        return new TodoData<T>();
                    }
                }
            }
            else
            {
                return new TodoData<T>();
            }
        }
        catch (JsonException ex)
        {
            System.Console.WriteLine($"Error deserialize file {jsonFilePath}: {ex.Message}");
            return new TodoData<T>();
        }
    }

    public override void Save<T>(TodoData<T> value)
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
