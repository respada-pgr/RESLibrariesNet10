using System.Text.Json.Serialization;

namespace _3_LibraryServicesNet10.Services.Crypto
{
    public class AesGcmPayload
    {
        [JsonPropertyName("iv")]
        public byte[] Iv { get; set; } = Array.Empty<byte>();

        [JsonPropertyName("data")]
        public byte[] Data { get; set; } = Array.Empty<byte>();
    }
}
