using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WS.WorkerExample.Data.Entities;

namespace WS.WorkerExample.Data.Mapping
{
    public class CentroDeCustoEntityMap : IEntityTypeConfiguration<CentroDeCusto>
    {
        public void Configure(EntityTypeBuilder<CentroDeCusto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(c => c.Responsaveis)
                   .WithOne()
                   .HasForeignKey<CentroDeCusto>(c => c.ResponsavelId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(c => c.Funcionarios)
                .WithOne(f => f.CentroDeCustos)
                .HasForeignKey(f => f.CentroDeCustoId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}