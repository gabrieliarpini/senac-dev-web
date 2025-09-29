using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Categorias.Commands
{
    public class AtivarCategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "É necessário informar o id da categoria")]
        public Guid CategoriaId { get; set; }
    }

    internal class AtivarCategroiaCommandHandler : IRequestHandler<AtivarCategoriaCommand, (string, bool)>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public AtivarCategroiaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<(string, bool)> Handle(AtivarCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);

            categoria.Ativar();

            await _categoriaRepository.AtualizarAsync(categoria);
            return ("Categoria ativada com sucesso.", true);
        }
    }


}
