using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Categorias.Commands
{
    public class DeletarCategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "É necessario informar o Id do usuário!")]
        public required Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "Id da categoria é obrigatório!")]
        public required Guid CategoriaId { get; set; }
    }

    internal class DeletarCategoriaCommandHandler : IRequestHandler<DeletarCategoriaCommand, (string, bool)>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public DeletarCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<(string, bool)> Handle(DeletarCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);
            if (categoria == null)
                return ("Categoria não encontrada.", false);

            if (categoria.UsuarioId != request.UsuarioId)
                return ("Categoria não pertence ao usuário.", false);

            await _categoriaRepository.RemoverAsync(categoria);
            return ("Categoria removida com sucesso.", true);
        }
    }
}