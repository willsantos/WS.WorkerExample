using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WS.WorkerExample.Data.Entities;

namespace WS.WorkerExample.Data.Mapping
{
    public class EmpresaEntityMap : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.RazaoSocial)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasMany(e => e.Contatos)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Enderecos)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}