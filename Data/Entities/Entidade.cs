namespace WS.WorkerExample.Data.Entities
{
    public class Entidade
    {
        public DateTimeOffset CriadoEm { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? AtualizadoEm { get; set; }
        public DateTimeOffset? ExcluidoEm { get; set; }
    }
}