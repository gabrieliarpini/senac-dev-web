using MediatR;
using MeuCorre.Application.UseCases.Usuarios.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MeuCorre.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        ///<summary>
        ///Cria um novo usuário.
        ///<param name="command"></param>
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CriarConta([FromBody] CriarContaCommad command)
        {
            var conta = await _mediator.Send(command);
            return CreatedAtAction(nameof(CriarConta), new { Id = conta.Id }, conta);

        }

    }
}