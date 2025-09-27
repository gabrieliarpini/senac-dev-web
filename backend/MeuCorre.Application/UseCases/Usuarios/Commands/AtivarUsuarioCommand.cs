using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Usuario.Commands
{
    public class AtivarUsuarioCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "É necessário informar o id do usuário.")]
        public Guid UsuarioId { get; set; }
    }

    internal class AtivarUsuarioCommandHandler : IRequestHandler<AtivarUsuarioCommand, (string, bool)>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public AtivarUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(string, bool)> Handle(AtivarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(request.UsuarioId);

            usuario.AtivarUsuario();

            await _usuarioRepository.AtualizarAsync(usuario);
            return ("Usuário ativado com sucesso.", true);
        }
    }


}
