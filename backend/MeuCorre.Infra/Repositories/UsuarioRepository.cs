using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace MeuCorre.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MeuDbContext _meuDbContext;

        public UsuarioRepository(MeuDbContext meuDbContext)
        {
            _meuDbContext = meuDbContext;
        }

        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
             _meuDbContext.Usuarios.Update(usuario);
             await _meuDbContext.SaveChangesAsync();
        }

        public async Task CriarUsuarioAsync(Usuario usuario)
        {
            await _meuDbContext.Usuarios.AddAsync(usuario);
            await _meuDbContext.SaveChangesAsync();
        }

        public async Task<Usuario?> ObterPorEmail(string email)
        {
            return await _meuDbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task RemoverUsuarioAsync(Usuario usuario)
        {
           _meuDbContext.Usuarios.Remove(usuario);
            return _meuDbContext.SaveChangesAsync();
        }
    }
}
 