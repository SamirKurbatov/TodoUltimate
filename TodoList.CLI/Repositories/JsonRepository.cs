using System.Text.Json;
using TodoList.CLI.Repositories;

namespace TodoList.CLI;

public class JsonRepository<T> : BaseRepository<T>
{
    private readonly string dataFolderPath = AppDomain.CurrentDomain.BaseDirectory;
    private string jsonFilePath;

    public JsonRepository(string fileName) : base(fileName)
    {
        jsonFilePath = Path.Combine(dataFolderPath, fileName);
    }

    public override TodoData<T> LoadData()
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
        catch (IOException ex)
        {
            System.Console.WriteLine($"Error deserialize file {jsonFilePath}: {ex.Message}");
            return new TodoData<T>();
        }
    }

    public override void SaveData(TodoData<T> value)
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
