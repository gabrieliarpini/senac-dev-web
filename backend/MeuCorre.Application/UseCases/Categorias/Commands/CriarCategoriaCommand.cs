using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Categorias.Commands
{
    public class CriarCategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage ="É necessario informar o Id do usuário!")]
        public required Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "Nome da categoria é obrigatório!")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Tipo da transação (despesa ou receita) é obrigatório!")]
        public required TipoTransacao Tipo { get; set; }
        public string? Descricao { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }

        
    }

    internal class CriarCategoriaCommandHandler : IRequestHandler<CriarCategoriaCommand, (string, bool)>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CriarCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<(string, bool)> Handle(CriarCategoriaCommand request, CancellationToken cancellationToken)
        {
           var existe = await _categoriaRepository.NomeExisteParaUsuarioAsync(request.Nome, request.Tipo, request.UsuarioId);
            if (existe)
            {
                return ("Categoria já cadastrada.", false);
            }
            var novaCategoria = new Categoria(
                request.UsuarioId,
                request.Nome,
                request.Tipo,
                request.Descricao,
                request.Cor,
                request.Icone
                );

            await _categoriaRepository.AdicionarAsync(novaCategoria);
            return ("Categoria cadastrada com sucesso.", true);

        }
    }

}
