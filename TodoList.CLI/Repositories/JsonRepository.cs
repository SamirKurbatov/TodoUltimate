using System.Text.Json;

namespace TodoList;

public class JsonRepository : BaseRepository
{
    private readonly string dataFolderPath = "C:/Users/user/Desktop/super-fortnight/TodoList.CLI/Data";

    public JsonRepository(string filePath) : base(filePath) { }

    public override Dictionary<TKey, TValue> Load<TKey, TValue>()
    {
        try
        {
            if (File.Exists(FilePath) == true)
            {
                using (var fs = new FileStream(FilePath, FileMode.OpenOrCreate))
                {
                    if (JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(fs) is Dictionary<TKey, TValue> items)
                    {
                        return items;
                    }
                    else
                    {
                        return new Dictionary<TKey, TValue>();
                    }
                }
            }
            else
            {
                return new Dictionary<TKey, TValue>();
            }
        }
        catch (JsonException ex)
        {
            System.Console.WriteLine($"Error deserialize file {FilePath}: {ex.Message}");
            return new Dictionary<TKey, TValue>();
        }
    }

    public override void Save<T>(T value)
    {
        if (Directory.Exists(dataFolderPath) == false)
        {
            Directory.CreateDirectory(dataFolderPath);
        }

        var options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        string jsonFilePath = Path.Combine(dataFolderPath, FilePath);

        string jsonString = JsonSerializer.Serialize(value, options);
        File.WriteAllText(jsonFilePath, jsonString);
    }
}
