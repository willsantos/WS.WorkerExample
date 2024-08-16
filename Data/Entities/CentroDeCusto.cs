namespace WS.WorkerExample.Data.Entities
{
    public class CentroDeCusto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public Guid? ResponsavelId { get; set; } // FK para Funcionario
        public Funcionario? Responsaveis { get; set; }  // O responsável por este Centro de Custo
        public List<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
    }
}