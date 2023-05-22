using System.Text.Json;

namespace TodoList;

public class JsonRepository : IRepository
{
    public Dictionary<TKey, TValue> Load<TKey, TValue>(string filePath)
    {
        try
        {
            if (File.Exists(filePath) == true)
            {
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
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
            System.Console.WriteLine($"Error deserialize file {filePath}: {ex.Message}");
            return new Dictionary<TKey, TValue>();
        }
    }

    public void Save<T>(string filePath, T value)
    {
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        string jsonString = JsonSerializer.Serialize(value, options);
        File.WriteAllText(filePath, jsonString);
    }
}
