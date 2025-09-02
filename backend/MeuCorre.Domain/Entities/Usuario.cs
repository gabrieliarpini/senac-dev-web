using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Entities
{
    public class Usuario : Entidade
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public bool Ativo { get; private set; }

        //Construtor para criar um novo usuário.
        //Construtor é a primeira coisa que é executada quando uma classe é instanciada.
        public Usuario(string nome, string email, DateTime dataNascimento, bool ativo)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Ativo = ativo;
        }

        //Regra negocio: Permite apenas usários maiores de 13 anos.
        private int CalcularIdade()
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - DataNascimento.Year;

            if (DataNascimento.Date > hoje.AddYears(-idade))
                idade--;

            return idade;
        }

        private bool TemIdadeMinima()
        {
            var resultado = CalcularIdade() >= 13;
            return resultado;
        }

        public void AtivarUsuario()
        {
            Ativo = true;
            AtualizarDataMoficacao();
        }

        public void InativarUsuario()
        {
            Ativo = false;
            AtualizarDataMoficacao();
        }
    }
}
