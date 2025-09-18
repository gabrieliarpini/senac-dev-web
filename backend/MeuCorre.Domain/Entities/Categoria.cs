using MeuCorre.Domain.Enums;
using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace MeuCorre.Domain.Entities
{
    public class Categoria : Entidade
    {
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public TipoTransacao TipoDaTransacao { get; private set; }
        public string? Descricao { get; private set; }
        public string? Cor { get; private set; }
        public string? Icone { get; private set; }
        public bool Ativo { get; set; }

        //Propriedade de navegação para a entidade Usuario pois o usuário pode ter várias categorias
        public virtual Usuario Usuario { get; private set; }

        public Categoria(Guid usuarioId, string nome, TipoTransacao tipo, string? descricao, string? cor, string? icone)
        {
            ValidarEntidadeCategoria(cor);

            UsuarioId = usuarioId;
            Nome = nome.ToUpper();
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            TipoDaTransacao = tipo;
            Ativo = true;

        }
        public void Ativar()
        {
            Ativo = true;
            AtualizarDataMoficacao();
        }
        public void Inativar()
        {
            Ativo = false;
            AtualizarDataMoficacao();
        }

        private void ValidarEntidadeCategoria(string cor)
        {
            if (string.IsNullOrEmpty(cor))
            {
                return;
            }

            var corRegex = new Regex(@"^#?([0-9a-fA-F]{3}){1,2}$");

            if (!corRegex.IsMatch(cor))
            {
                throw new Exception("Cor inválida. Deve ser um código hexadecimal.");
            }
        }

        public void AtualizarInformacoes(Guid categoriaId, string nome, TipoTransacao tipo, string? descricao, string? cor, string? icone)
        {
            Nome = nome.ToUpper();
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            TipoDaTransacao = tipo;
            AtualizarDataMoficacao();
        }
    }
}
