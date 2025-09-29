namespace MeuCorre.Domain.Entities
{
    public class Conta : Entidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; } // Ex: "Receita ou despesa"
        public string Moeda { get; set; } // Ex: "BRL"
        public decimal Saldo { get; set; }
        public decimal? Limite { get; set; }
        public string TipoLimite { get; set; } // Ex: "Total"
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

    }
}
