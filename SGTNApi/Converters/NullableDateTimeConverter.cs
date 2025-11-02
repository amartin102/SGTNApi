using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGTNApi.Converters
{
    public class NullableDateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var s = reader.GetString();
                if (string.IsNullOrWhiteSpace(s))
                    return null;

                if (DateTime.TryParse(s, out var dt))
                    return dt;

                // try parsing ISO 8601
                if (DateTime.TryParseExact(s, "o", null, System.Globalization.DateTimeStyles.RoundtripKind, out dt))
                    return dt;

                throw new JsonException($"Invalid DateTime format: '{s}'");
            }

            if (reader.TokenType == JsonTokenType.Null)
                return null;

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString("o"));
            else
                writer.WriteNullValue();
        }
    }
}
