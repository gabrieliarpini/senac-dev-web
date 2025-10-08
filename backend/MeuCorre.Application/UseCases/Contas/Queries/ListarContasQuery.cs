using MediatR;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Contas.Queries
{
    public class ListarContasQuery : IRequest<List<ContaResumoResponse>>
    {
        public Guid UsuarioId { get; set; }
        public TipoConta? FiltrarPorTipo { get; set; }
        public bool ApenasAtivas { get; set; } = false;
        public string OrdenarPor { get; set; } = "Nome";

    }

    public class ContaResumoResponse
    {
        public Guid ContaId { get; set; }
        public string Nome { get; set; }
        public TipoConta Tipo { get; set; }
        public string? Cor { get; set; }
        public string Moeda { get; set; }
        public decimal Saldo { get; set; }
        public TipoLimite? Limite { get; set; }
        public decimal? LimiteDisponivel { get; set; } // Calculado para cartões
        public int? DiaVencimento { get; set; }
        public bool Ativa { get; set; }
    }

    public class ListarContasQueryHandler : IRequestHandler<ListarContasQuery, List<ContaResumoResponse>>
    {
        private readonly IContaRepository _contaRepository;

        public ListarContasQueryHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<List<ContaResumoResponse>> Handle(ListarContasQuery request, CancellationToken cancellationToken)
        {
            var contas = await _contaRepository.ObterPorUsuarioAsync(request.UsuarioId);

            // Filtros opcionais
            if (request.FiltrarPorTipo.HasValue)
                contas = contas.Where(c => c.Tipo == request.FiltrarPorTipo.Value).ToList();

            if (request.ApenasAtivas)
                contas = contas.Where(c => c.Ativo).ToList();

            // Montagem do resumo com cálculo de limite disponível
            var contasResumo = contas.Select(c =>
            {
                decimal? limiteDisponivel = null;

                if (c.Tipo == TipoConta.CartaoCredito && c.Limite.HasValue)
                {
                    limiteDisponivel = c.Limite.Value - c.Saldo;
                }

                return new ContaResumoResponse
                {
                    ContaId = c.Id,
                    Nome = c.Nome,
                    Tipo = c.Tipo,
                    Cor = c.Cor,
                    Moeda = c.Moeda,
                    Saldo = c.Saldo,
                    Limite = (TipoLimite?)c.Limite,
                    LimiteDisponivel = limiteDisponivel,
                    DiaVencimento = c.DiaVencimento,
                    Ativa = c.Ativo
                };
            }).ToList();

            // Ordenação
            contasResumo = request.OrdenarPor?.ToLower() switch
            {
                "saldo" => contasResumo.OrderByDescending(c => c.Saldo).ToList(),
                "tipo" => contasResumo.OrderBy(c => c.Tipo).ToList(),
                _ => contasResumo.OrderBy(c => c.Nome).ToList(),
            };

            return contasResumo;
        }
    }

}