using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;

namespace MeuCorre.Domain.Interfaces.Repositories
{
  
    public interface ICategoriaRepository
    {
        //Retorno do banco de ddos de  uma categoria que possua a Id informado
        Task<Categoria>ObterPorIdAsync(Guid categoriaId);

        //Retorna do banco de dados todas as categorias que pertençam ao usuário informado
        Task<IEnumerable<Categoria>> ObterTodosAsync(Guid usuarioId);

        //Verificar se uma categoria existe no banco de dados com o Id informado
        //SELECT * FROM categoria WHERE Id = 5
        Task<bool> ExisteAsync(Guid categoriaId);

        //Verifica se já existe uma categoria com o mesmo nome e tipo para o usuário
        Task<bool>NomeExisteParaUsuarioAsync(string nome,TipoTransacao tipo, Guid usuarioId);

        //Adiciona uma nova categoria no banco de dados
        Task AdicionarAsync(Categoria categoria);

        //Atualiza os dados de uma categoria existente
        Task AtualizarAsync(Categoria categoria);

        //Remove uma categoria do banco de dados
        Task RemoverAsync(Guid categoriaId);

    }
}
