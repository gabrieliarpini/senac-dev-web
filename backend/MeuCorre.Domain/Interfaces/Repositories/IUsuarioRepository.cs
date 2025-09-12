using MeuCorre.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task CriarUsuarioAsync(Usuario usuario);//INSERT    
        Task AtualizarUsuarioAsync(Usuario usuario);//UPDATE
        Task RemoverUsuarioAsync(Usuario usuario);//DELETE
        //?significa que o select pode retornar nulo, ou seja, o usuário pode não ser encontrado
        Task<Usuario?> ObterPorEmail(string email);//SELECT POR EMAIL
        Task ObterPorIdAsync(Guid id);
    }
}
