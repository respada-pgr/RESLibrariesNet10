using System.Text.Json;
using System.Text.Json.Serialization;

namespace _2_LibraryUtils.Converters
{
    public class C_JsonConverter : JsonConverter<byte[]>
    {
        public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // 1. Si viene como array numérico de JS: [164, 137, 205...]
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var bytes = new List<byte>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                        return bytes.ToArray();

                    if (reader.TokenType == JsonTokenType.Number)
                        bytes.Add(reader.GetByte());
                }
            }

            // 2. Si viene como String Base64 estándar
            if (reader.TokenType == JsonTokenType.String)
            {
                string? base64 = reader.GetString();
                return string.IsNullOrEmpty(base64) ? Array.Empty<byte>() : Convert.FromBase64String(base64);
            }

            throw new JsonException($"Token no esperado '{reader.TokenType}' al deserializar byte[].");
        }

        public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
        {
            // Escribe como array numérico [1, 2, 3...] para mantener compatibilidad con la web
            writer.WriteStartArray();
            foreach (byte b in value)
            {
                writer.WriteNumberValue(b);
            }
            writer.WriteEndArray();
        }
    }
}
