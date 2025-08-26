using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Core.Entities
{
    internal class Categoria : Usuario
    {
		public Categoria(string nome, string email, DateTime dataNascimento, bool ativo) : base(nome, email, dataNascimento, ativo)
		{
		}

		public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
    }
}
