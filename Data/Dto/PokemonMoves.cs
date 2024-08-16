namespace WS.WorkerExample.Data.Dto
{
    public class PokemonMoves
    {
        public List<Move> Moves { get; set; } = new List<Move>();
    }

    public class Move
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}