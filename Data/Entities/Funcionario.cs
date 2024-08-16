namespace WS.WorkerExample.Data.Entities
{
    public class Funcionario : Entidade
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public Guid? CentroDeCustoId { get; set; } // FK para Centro de custo
        public List<Contato> Contatos { get; set; } = new List<Contato>();
        public List<Endereco> Enderecos { get; set; } = new List<Endereco>();
        public CentroDeCusto CentroDeCustos { get; set; }
    }
}