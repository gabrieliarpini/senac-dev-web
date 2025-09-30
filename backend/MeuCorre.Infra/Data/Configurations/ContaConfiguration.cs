using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuCorre.Infra.Data.Configurations
{
    internal class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            //Define o nome da tabela no banco de dados.
            builder.ToTable("Contas");

            //Define a chave primária.
            builder.HasKey(conta => conta.Id);

            //Define as propriedades e suas configurações.
            builder.Property(conta => conta.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(conta => conta.Cor)
                .IsRequired()
                .HasMaxLength(7);

            builder.Property(conta => conta.Tipo)
                .IsRequired();

            builder.Property(conta => conta.Moeda)
                .IsRequired();

            builder.Property(conta => conta.Saldo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(conta => conta.Limite)
                .IsRequired();

            builder.Property(conta => conta.DiaVencimento)
                .IsRequired();

            builder.Property(conta => conta.VencimentoPrimeiraFatura)
                .IsRequired();

            builder.Property(conta => conta.FechamentoFatura)
                .IsRequired();

            builder.Property(conta => conta.SaldoFaturaAnterior)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(conta => conta.CredorDevedor)
               .IsRequired();

            builder.HasIndex(conta => conta.PreverDebitoNaConta)
                .IsUnique();

            builder.Property(conta => conta.UsuarioId)
                .IsRequired();

            builder.Property(conta => conta.Ativo)
                .IsRequired(false);

            builder.Property(conta => conta.DataCriacao)
                .IsRequired();

            builder.Property(conta => conta.DataAtualizacao)
                .IsRequired(false);

            //Chaves Estrangeiras FK
            //Define o relacionamento entre Conta e Usuario
            builder.HasOne(conta => conta.Usuario)
                .WithMany(usuario => usuario.Conta)
                .HasForeignKey(conta => conta.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    }
}
