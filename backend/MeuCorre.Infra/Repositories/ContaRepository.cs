using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MeuCorre.Infra.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly MeuDbContext _meuDbContext;
        public ContaRepository(MeuDbContext meuDbContext)
        {
            _meuDbContext = meuDbContext;
        }

        public async Task AdicionarAsync(Conta conta)
        {
            _meuDbContext.Set<Conta>().Add(conta);
            await _meuDbContext.SaveChangesAsync();

        }

        public async Task AtualizarAsync(Conta conta)
        {
            _meuDbContext.Contas.Update(conta);
            await _meuDbContext.SaveChangesAsync();
        }

        public async Task<decimal> CalcularSaldoTotalAsync(Guid usuarioId)
        {
            var saldoTotal = await _meuDbContext.Contas
                .Where(c => c.UsuarioId == usuarioId)
                .SumAsync(c => c.Saldo);

            return saldoTotal;
        }

        public async Task<bool> ExisteContaComNomeAsync(Guid contaId, string nome, Guid? contaIdExcluir = null)
        {
            var existe = await _meuDbContext.Contas
                .AnyAsync(c => c.Id == contaId);

            return existe;
        }

        public async Task<Conta?> ObterPorIdAsync(Guid contaId)
        {
            return await _meuDbContext.Contas.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Conta?> ObterPorIdEUsuarioAsync(Guid contaId, Guid usuarioId)
        {
            var conta = await _meuDbContext.Contas.FindAsync(contaId,usuarioId);
            return conta;
        }

        public async Task<List<Conta>> ObterPorTipoAsync(Guid usuarioId, TipoConta tipo)
        {
            var contas = await _meuDbContext.Contas
                .Where(c => c.UsuarioId == usuarioId && c.Tipo == tipo)
                .ToListAsync();

            return contas;
        }

        public async Task<List<Conta>> ObterPorUsuarioAsync(Guid usuarioId, bool apenasAtivas = true)
        {

            var usuariosAtivos = _meuDbContext.Contas
                .Where(c => c.UsuarioId == usuarioId &&
                       c.Ativo == apenasAtivas);

            return await usuariosAtivos.ToListAsync();
        }
    }
}
