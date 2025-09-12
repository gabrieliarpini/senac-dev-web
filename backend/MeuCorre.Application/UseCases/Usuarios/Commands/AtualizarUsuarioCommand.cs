using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Usuarios.Commands
{
    public class AtualizarUsuarioCommand : IRequest<(string, bool)>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }

    }

    internal class AtualizarUsuarioCommandHandler : IRequestHandler<AtualizarUsuarioCommand, (string, bool)>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public AtualizarUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(string, bool)> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar usuário existente
            var usuarioExiste = await _usuarioRepository.ObterPorIdAsync(request.Id);
            if (usuarioExiste == null)
            {
                return ("Usuário não encontrado.", false);
            }

            // 2. Validar se o usuário possui dados essenciais (ex: Email cadastrado)
            if (string.IsNullOrWhiteSpace(usuarioExiste.Email))
            {
                return ("Usuário não possui email cadastrado, impossível atualizar o perfil.", false);
            }

            // 3. Alterar apenas nome e data de nascimento
            usuarioExiste.Nome = request.Nome;
            usuarioExiste.DataNascimento = request.DataNascimento;
            usuarioExiste.Email = request.Email;

            // 4. Persistir alterações
            await _usuarioRepository.AtualizarUsuarioAsync(usuarioExiste);

            return ("Perfil atualizado com sucesso.", true);
        }
    }

}
