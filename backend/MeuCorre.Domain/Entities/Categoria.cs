using MeuCorre.Domain.Enums;

namespace MeuCorre.Domain.Entities
{
    public class Categoria : Entidade
    {
        public string Nome { get; private set; }
        public TipoTransacao Tipo { get; private set; }
        public string Descricao { get; private set; }
        public string Cor { get; private set; }
        public string Icone { get; private set; }
        public Guid UsuarioId { get; private set; }
       

        public Categoria(string nome, TipoTransacao tipo, string descricao,string cor, string icone, Guid usuarioId)
        {
           
            Nome = nome;
            Tipo = tipo;
            Descricao = descricao;
            Cor = cor;
            Icone= icone;
            UsuarioId = usuarioId;

        }

    }
}
