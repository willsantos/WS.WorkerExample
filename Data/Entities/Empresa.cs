namespace WS.WorkerExample.Data.Entities
{
    public class Empresa : Entidade
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string RazaoSocial { get; set; } = string.Empty;
        public List<Contato> Contatos { get; set; } = new List<Contato>();
        public List<Endereco> Enderecos { get; set; } = new List<Endereco>();
    }
}