using Microsoft.EntityFrameworkCore;
using WS.WorkerExample.Data.Entities;
using WS.WorkerExample.Data.Mapping;

namespace WS.WorkerExample.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<CentroDeCusto> CentrosDeCusto { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FuncionarioEntityMap());
            modelBuilder.ApplyConfiguration(new EmpresaEntityMap());
            modelBuilder.ApplyConfiguration(new CentroDeCustoEntityMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}