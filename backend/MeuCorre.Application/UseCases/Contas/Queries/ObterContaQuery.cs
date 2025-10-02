using MeuCorre.Domain.Entities;
using MediatR;
using System;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Contas.Queries
{
    public class ObterContaQuery : IRequest<ContaDetalheResponse>
    {
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }

        public ObterContaQuery(Guid contaId, Guid usuarioId)
        {
            ContaId = contaId;
            UsuarioId = usuarioId;
        }
    }

    public class ObterContaQueryHandler : IRequestHandler<ObterContaQuery, ContaDetalheResponse>
    {
        private readonly IContaRepository _contaRepository;

        public ObterContaQueryHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<ContaDetalheResponse> Handle(ObterContaQuery request, CancellationToken cancellationToken)
        {
            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId);

            if (conta == null)
                throw new Exception("Conta não encontrada.");

            if (conta.UsuarioId != request.UsuarioId)
                throw new UnauthorizedAccessException("Conta não pertence ao usuário.");

            return new ContaDetalheResponse
            {
                Id = conta.Id,
                Nome = conta.Nome,
                Cor = conta.Cor,
                Tipo = conta.Tipo,
                Moeda = conta.Moeda,
                Saldo = conta.Saldo,
                Limite = (TipoLimite)conta.Limite,
                DiaVencimento = conta.DiaVencimento,
                VencimentoPrimeiraFatura = conta.VencimentoPrimeiraFatura,
                FechamentoFatura = conta.FechamentoFatura,
                SaldoFaturaAnterior = conta.SaldoFaturaAnterior,
                CredorDevedor = conta.CredorDevedor,
                PreverDebitoNaConta = conta.PreverDebitoNaConta,
                UsuarioId = conta.UsuarioId,
                Ativo = conta.Ativo,
                DataCriacao = conta.DataCriacao,
                DataAtualizacao = conta.DataAtualizacao,
                Descricao = conta.Descricao,
                Icone = conta.Icone
            };
        }
    }

    public class ContaDetalheResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
        public TipoConta Tipo { get; set; }
        public string Moeda { get; set; }
        public decimal Saldo { get; set; }
        public TipoLimite Limite { get; set; }
        public int? DiaVencimento { get; set; }
        public DateTime? VencimentoPrimeiraFatura { get; set; }
        public DateTime? FechamentoFatura { get; set; }
        public decimal? SaldoFaturaAnterior { get; set; }
        public string CredorDevedor { get; set; }
        public bool PreverDebitoNaConta { get; set; }
        public Guid UsuarioId { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public string Descricao { get; set; }
        public string Icone { get; set; }
    }
}