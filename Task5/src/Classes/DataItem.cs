using System.Collections.Generic;
using System.Text.Json.Serialization;

public class DataItem
{
    [JsonPropertyName("bytes")]
    public List<byte> Byte { get; set; }

    [JsonPropertyName("variable")]
    public string Variable { get; set; }

    [JsonPropertyName("format")]
    public string Format { get; set; }

    [JsonPropertyName("unit")]
    public string Unit { get; set; }

    [JsonPropertyName("coefficient")]
    public double? Coefficient { get; set; }

    [JsonPropertyName("offset")]
    public double? Offset { get; set; }
}
