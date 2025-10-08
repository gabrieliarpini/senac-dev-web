using MediatR;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class AtualizarContaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "Id da conta é obrigatório!")]
        public required Guid ContaId { get; set; }

        [Required(ErrorMessage = "Nome da conta é obrigatório!")]
        public required string Nome { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }
        public decimal? Limite { get; set; }
        public DateTime? FechamentoFatura { get; set; }
        public int? DiaVencimento { get; set; }
        public Guid Id { get; set; }
    }

    internal class AtualizarContaCommandHandler : IRequestHandler<AtualizarContaCommand, (string, bool)>
    {
        private readonly IContaRepository _contaRepository;
        public AtualizarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<(string, bool)> Handle(AtualizarContaCommand request, CancellationToken cancellationToken)
        {
            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId);
            if (conta == null)
            {
                return ("Conta não encontrada.", false);
            }


            conta.AtualizarInformacoes(
                request.ContaId,
                request.Nome,
                request.Cor,
                request.Icone,
                request.Limite,
                request.FechamentoFatura,
                request.DiaVencimento);
            await _contaRepository.AtualizarAsync(conta);
            return ("Conta atualizada com sucesso.", true);
        }
    }
}
