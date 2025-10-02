using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class ExcluirContaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "É necessário informar o id da conta")]
        public Guid ContaId { get; set; }

        [Required(ErrorMessage = "É necessário informar o id do usuário")]
        public Guid UsuarioId { get; set; }
        public bool Confirmar { get; set; }
    }

    internal class ExcluirContaCommandHandler : IRequestHandler<ExcluirContaCommand, (string, bool)>
    {
        private readonly IContaRepository _contaRepository, _transacaoRepository;
        public ExcluirContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }
        public async Task<(string, bool)> Handle(ExcluirContaCommand request, CancellationToken cancellationToken)
        {
            var conta = await _contaRepository.ObterPorIdAsync(request.ContaId, request.UsuarioId);

            if (conta == null)
                return ("Conta não encontrada.", false);

            if (conta.Saldo != 0)
                return ("Não é possível excluir a conta com saldo diferente de zero.", false);

            if (!request.Confirmar)
                return ("Confirmação obrigatória para excluir a conta.", false);

            await _contaRepository.ExcluirAsync(conta);
            return ("Conta excluída com sucesso.", true);
        }
    }
}
