namespace WS.WorkerExample.Data.Entities
{
    public class Contato
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Tipo { get; set; } = string.Empty;
        public string Valor { get; set; } = string.Empty;
    }
}