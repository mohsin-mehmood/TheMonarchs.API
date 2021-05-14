using System.Text.Json.Serialization;

namespace TheMonarchs.Infrastructure.Models
{
    public class MonarchJsonModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nm")]
        public string Name { get; set; }

        [JsonPropertyName("cty")]
        public string City { get; set; }

        [JsonPropertyName("hse")]
        public string House { get; set; }

        [JsonPropertyName("yrs")]
        public string Yrs { get; set; }
    }
}
