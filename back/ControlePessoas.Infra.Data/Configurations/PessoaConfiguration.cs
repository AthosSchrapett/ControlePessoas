using ControlePessoas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlePessoas.Infra.Data.Configurations;
internal class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("pessoas");
        builder.HasKey(p => p.Id);

        #region Base
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.CreatedAt).HasColumnName("created_at");
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");
        builder.Property(p => p.IsDeleted).HasColumnName("is_deleted");
        #endregion

        builder.Property(p => p.Nome)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(p => p.Idade)
            .HasColumnName("idade")
            .IsRequired();

        builder.Property(p => p.Sexo)
            .HasColumnName("sexo")
            .IsRequired()
            .HasMaxLength(1);

        builder.Property(p => p.Peso)
            .HasColumnName("peso")
            .IsRequired()
            .HasColumnType("decimal(5,2)");

        builder.Property(p => p.Altura)
            .HasColumnName("altura")
            .HasColumnType("decimal(4,2)");

        builder.Property(p => p.Idoso)
            .HasColumnName("idoso")
            .IsRequired();
    }
}
