using System.Text.Json;
using System.Text.Json.Serialization;

namespace Melin.Server.Models.Binders;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class ReferenceConverter : JsonConverter<Reference>
{
    public override Reference ReadJson(JsonReader reader, Type objectType, Reference existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);
        string referenceType = obj["type"]?.ToString();  // Get the Type field (e.g., Book or Artwork)

        // Based on the Type field, deserialize into the appropriate subclass
        if (referenceType == ReferenceType.Book.ToString())
        {
            var o = obj.ToObject<Book>(serializer);
            if (o != null)
            {
                return o;
            }
        }
        else if (referenceType == ReferenceType.Artwork.ToString())
        {
            return obj.ToObject<Artwork>(serializer);
        }

        throw new JsonSerializationException($"Unknown reference type: {referenceType}");
    }

    public override void WriteJson(JsonWriter writer, Reference value, JsonSerializer serializer)
    {
        JObject jo = JObject.FromObject(value, serializer);
        jo.WriteTo(writer);
    }
}
