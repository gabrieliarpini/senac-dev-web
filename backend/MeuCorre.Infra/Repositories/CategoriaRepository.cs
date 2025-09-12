using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Context;

namespace MeuCorre.Infra.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public Task AdicionarAsync(Categoria categoria)
        {
            await _meuDbContext.Categoria.AddAsync(categoria);
            await _meuDbContext.SaveChangesAsync();
        }

        public Task AtualizarAsync(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExisteAsync(Guid categoriaId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> NomeExisteParaUsuarioAsync(string nome, TipoTransacao tipo, Guid usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<Categoria> ObterPorIdAsync(Guid categoriaId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Categoria>> ObterTodosAsync(Guid usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task RemoverAsync(Guid categoriaId)
        {
            throw new NotImplementedException();
        }
    }
}
