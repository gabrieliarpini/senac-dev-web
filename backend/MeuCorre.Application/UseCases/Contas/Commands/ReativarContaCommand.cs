using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class ReativarContaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "É necessário informar o id da conta")]
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }
    }

    internal class ReativarContaCommandHandler : IRequestHandler<ReativarContaCommand, (string, bool)>
    {
        private readonly IContaRepository _contaRepository;
        public ReativarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<(string, bool)> Handle(ReativarContaCommand request, CancellationToken cancellationToken)
        {
            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId);
            conta.ReativarConta();

            await _contaRepository.AtualizarAsync(conta);
            return ("Conta ativada com sucesso.", true);
        }
    }
}
