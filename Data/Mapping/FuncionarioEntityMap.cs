using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WS.WorkerExample.Data.Entities;

namespace WS.WorkerExample.Data.Mapping
{
    public class FuncionarioEntityMap : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(f => f.Contatos)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(f => f.Enderecos)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.CentroDeCustos)
                   .WithMany(c => c.Funcionarios)
                   .HasForeignKey(f => f.CentroDeCustoId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}