using MeuCorre.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MeuCorre.Domain.Entities
{
    public class Conta : Entidade
    {
        private string? cor;
        private decimal? limite;
        private TipoConta tipo;

        public Conta(Guid id, string nome, TipoConta tipo, decimal saldo, string? cor, decimal? limite, int? diaVencimento) : base(id)
        {
            UsuarioId = id;
            Nome = nome;
            Tipo = tipo;
            Saldo = saldo;
            this.cor = cor;
            this.limite = limite;
            DiaVencimento = diaVencimento;
            
        }

        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nome { get; set; }
        public string? Cor { get; set; } // Ex: "#FFFFFF"
        [Required]
        public TipoConta Tipo { get; set; } // Ex: "Cartão de credito"
        public string Moeda { get; set; } // Ex: "BRL"
        public decimal Saldo { get; set; }
        public decimal? Limite { get; set; }
        [Range(1, 31)]
        public int? DiaVencimento { get; set; }
        public DateTime? VencimentoPrimeiraFatura { get; set; }
        public DateTime? FechamentoFatura { get; set; }
        public decimal? SaldoFaturaAnterior { get; set; }
        public string CredorDevedor { get; set; } // Ex: "Devedor"
        public bool PreverDebitoNaConta { get; set; }
        public Guid UsuarioId { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public Usuario Usuario { get; set; }
        public string Descricao { get; set; }
        public string Icone { get; set; }
        public string Transacao { get; set; }

        public void AtualizarInformacoes(Guid contaId, string nome, string? cor, string? icone, decimal? limite, DateTime? fechamentoFatura, int? diaVencimento)
        {
            Nome = nome.ToUpper();
            Cor = cor;
            Icone = icone;
            Limite = limite;
            FechamentoFatura = fechamentoFatura;
            DiaVencimento = diaVencimento;
            AtualizarDataMoficacao();
        }

        public bool CorHexValida()
        {
            return string.IsNullOrEmpty(Cor) || Regex.IsMatch(Cor, "^#([A-Fa-f0-9]{6})$");
        }

        public void Inativar()
        {
            Ativo = false;
            AtualizarDataMoficacao();
        }

        public void ReativarConta()
        {
            Ativo = true;
            AtualizarDataMoficacao();
        }
    }


}
