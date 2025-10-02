using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;

namespace MeuCorre.Domain.Interfaces.Repositories
{
    public interface IContaRepository 
    {
        Task<List<Conta>> ObterPorUsuarioAsync(Guid usuarioId, bool apenasAtivas = true);
        Task<List<Conta>> ObterPorTipoAsync(Guid usuarioId, TipoConta tipo);
        Task<Conta?> ObterPorIdEUsuarioAsync(Guid contaId, Guid usuarioId);
        Task<bool> ExisteContaComNomeAsync(Guid usuarioId, string nome, Guid? contaIdExcluir = null);
        Task<decimal> CalcularSaldoTotalAsync(Guid usuarioId);
        Task AdicionarAsync(Conta conta);
        Task<Conta?> ObterPorIdAsync(Guid contaId, Guid usuarioId);
        Task AtualizarAsync(Conta conta);
        Task<Conta> ObterPorIdAsync(object contaId);
        Task ExcluirAsync(Conta conta);
        Task ObterPorContaIdAsync(Guid contaId);
    }
}
