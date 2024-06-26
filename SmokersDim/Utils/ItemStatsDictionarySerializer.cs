using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ItemStatsDictionarySerializer : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Dictionary<string, StatEntry>);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;

        var jt = JToken.Load(reader);
        return JsonConvert.DeserializeObject<Dictionary<string, StatEntry>>(jt.Value<String>());
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}