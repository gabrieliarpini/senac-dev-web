using MediatR;
using MeuCorre.Application.UseCases.Contas.Commands;
using MeuCorre.Application.UseCases.Contas.Queries;
using MeuCorre.Application.UseCases.Usuarios.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MeuCorre.Controllers
{

    [ApiController]
    [Route("api/v1/contas")]
    public class ContasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarConta([FromBody] CriarContaCommand command)
        {
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return Ok(mensagem);
            }
            else
            {
                return Conflict(mensagem);
            }
        }

        [HttpGet("{ContaId}")]
        public async Task<IActionResult> AtualizarConta([FromBody] AtualizarContaCommand command)
        {
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return Ok(mensagem);
            }
            else
            {
                return NotFound(mensagem);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> InativarConta(Guid id)
        {
            var command = new InativarContaCommand { ContaId = id };
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(mensagem);
            }
        }

        [HttpPatch("Reativar/{id}")]
        public async Task<IActionResult> ReativarConta(Guid id)
        {
            var command = new ReativarContaCommand { ContaId = id };
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(mensagem);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> ExcluirConta([FromBody] ExcluirContaCommand command)
        {
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(mensagem);
            }
        }

        [HttpGet("{UsuarioId}")]
        public async Task<IActionResult> ObterContaPorUsuario([FromQuery] ListarContasQuery query)
        {
            var categorias = await _mediator.Send(query);
            return Ok(categorias);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> ObterContasPorId([FromQuery] ObterContaQuery query)
        {
            var categorias = await _mediator.Send(query);
            return Ok(categorias);
        }
    }
}
