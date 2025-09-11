using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Usuarios.Commands
{
    public class CriarUsuarioCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória!")]
        [MaxLength(6,ErrorMessage="Senha deve ter no mínimo 6 caracteres ")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Data de Nascimento é obrigatória!")]
        public DateTime DataNascimento { get; set; }
    }

    internal class CriarUsuarioCommandHandler : IRequestHandler<CriarUsuarioCommand, (string,bool)>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public CriarUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(string,bool)> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuarioExixtente = await _usuarioRepository.ObterPorEmail(request.Email);
            if (usuarioExixtente != null)
            {
                return ("Já existe um usuário cadastrado com este email.",false);
            }

            var ano = DateTime.Now.Year;
            var idade = ano - request.DataNascimento.Year;
            if (idade < 13)
            {
                return ("Usuário deve ter no mínimo 13 anos.",false);
            }

            var novoUsuario = new Usuario(
                request.Nome,
                request.Email, 
                request.Senha,
                request.DataNascimento,
                true);
            await _usuarioRepository.CriarUsuarioAsync(novoUsuario);
            return ("Usuário criado com sucesso.",true);
        }
    }
}
