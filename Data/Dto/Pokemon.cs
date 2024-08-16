using System.Text.Json.Serialization;

namespace WS.WorkerExample.Data.Dto
{
    public class Pokemon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("moves")]
        public List<PokemonMoves> Moves { get; set; } = new List<PokemonMoves>();
    }
}