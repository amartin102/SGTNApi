using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGTNApi.Converters
{
    public class NullableTimeSpanConverter : JsonConverter<TimeSpan?>
    {
        public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var s = reader.GetString();
                if (string.IsNullOrWhiteSpace(s))
                    return null;

                if (TimeSpan.TryParse(s, out var ts))
                    return ts;

                // Try to parse ISO time components (hh:mm[:ss])
                throw new JsonException($"Invalid TimeSpan format: '{s}'");
            }

            if (reader.TokenType == JsonTokenType.Null)
                return null;

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString());
            else
                writer.WriteNullValue();
        }
    }
}
