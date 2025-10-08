using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class InativarContaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "É necessário informar o id da conta")]
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }
    }

    internal class InativarContaCommandHandler : IRequestHandler<InativarContaCommand, (string, bool)>
    {
        private readonly IContaRepository _contaRepository;
        public InativarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<(string, bool)> Handle(InativarContaCommand request, CancellationToken cancellationToken)
        {
            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId, request.UsuarioId);

            if (conta == null)
                return ("Conta não encontrada.", false);

            if (conta.Saldo > 0)
                return ("Não é possível inativar a conta com saldo positivo.", false);

            conta.Inativar();

            await _contaRepository.AtualizarAsync(conta);
            return ("Conta desativada com sucesso.", true);

        }
    }
}
