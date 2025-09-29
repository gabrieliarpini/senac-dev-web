using MediatR;
using MeuCorre.Application.UseCases.Categorias.Commands;
using MeuCorre.Application.UseCases.Categorias.Dtos;
using MeuCorre.Application.UseCases.Categorias.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MeuCorre.Controllers
{
    [ApiController]
    [Route("[controller]")]

    
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        ///<summary>
        /// Cria novas categorias para o usuários
        ///<summary>
        ///Cria um novo usuário.
        ///<para name="command"> Os dados da nova categoriam</param>
        /// <return>retorna uma categoria criarda</returns>
        /// 
        [HttpPost]
        [ProducesResponseType(typeof(CategoriaDto), 201)]
        [ProducesResponseType (400)]
        [ProducesResponseType(409)]


        public async Task<IActionResult> CriarCategoria([FromBody] CriarCategoriaCommad command)
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

        [HttpPut]
        public async Task<IActionResult> AtualizarCategoria([FromBody] AtualizarCategoriaCommand command)
        {
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return Ok(mensagem);
            }
            else
            {
                return BadRequest(mensagem);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarCategoria([FromBody] DeletarCategoriaCommand command)
        {
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return Ok(mensagem);
            }
            else
            {
                return BadRequest(mensagem);
            }
        }

        [HttpPatch("ativar/{id}")]
        public async Task<IActionResult> AtivarCategoria(Guid Id)
        {
            var command = new AtivarCategoriaCommand { CategoriaId = Id };
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return Ok(mensagem);
            }
            else
            {
                return BadRequest("Categoria possivelmente não encontrada.");
            }
        }

        [HttpPatch("inativar/{id}")]
        public async Task<IActionResult> InativarCategoria(Guid Id)
        {
            var command = new InativarCategoriaCommand { CategoriaId = Id };
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
        [HttpGet]
        public async Task<ActionResult> ObterCategoriaPorUsuario([FromQuery]ListarTodasCategoriasQuery query)
        {
            var categorias = await _mediator.Send(query);
            return Ok(categorias);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterCategoriaPorId(Guid id)
        {
            var query = new ObterCategoriaQuery() { CategoriaId = id };
            var categoria = await _mediator.Send(query);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }
            return Ok(categoria);

        }

    }
}
