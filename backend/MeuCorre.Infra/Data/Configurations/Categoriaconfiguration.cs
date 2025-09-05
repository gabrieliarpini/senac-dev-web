using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuCorre.Infra.Data.Configurations
{
    class Categoriaconfiguration
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            //Define o nome da tabela no banco de dados.
            builder.ToTable("Categorias");

            builder.HasIndex(c => c.Nome)
                .IsUnique();

            builder.Property(categoria => categoria.Tipo)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(categoria => categoria.Descricao)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(categoria => categoria.Cor)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(categoria => categoria.Icone)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
