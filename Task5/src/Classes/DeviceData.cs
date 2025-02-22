using System.Collections.Generic;
using System.Text.Json.Serialization;

public class DeviceData
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("data")]
    public List<DataItem> Data { get; set; }
}
