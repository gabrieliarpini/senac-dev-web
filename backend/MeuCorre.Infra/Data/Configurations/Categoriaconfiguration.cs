using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuCorre.Infra.Data.Configurations
{
    internal class Categoriaconfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            //Define o nome da tabela no banco de dados.
            builder.ToTable("Categorias");

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(categoria => categoria.Tipo)
                .IsRequired();

            builder.Property(categoria => categoria.Descricao)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(categoria => categoria.Cor)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(categoria => categoria.Icone)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(usuario => usuario.DataCriacao)
                .IsRequired();

            builder.Property(usuario => usuario.DataAtualizacao)
                .IsRequired(false);

            //Chaves Estrangeiras FK
            //Define o relacionamento entre Categoria e Usuario
            builder.HasOne(categoria => categoria.Usuario)
                .WithMany(usuario => usuario.Categorias)
                .HasForeignKey(categoria => categoria.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade); 

        }
    }
       
}
