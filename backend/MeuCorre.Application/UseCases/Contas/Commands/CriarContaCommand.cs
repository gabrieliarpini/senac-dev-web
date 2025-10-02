using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public class CriarContaCommand : IRequest<CriarContaResponse>
    {
        [Required(ErrorMessage = "É necessário informar o id do usuário")]
        public Guid UsuarioId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Nome { get; set; }
        public TipoConta Tipo { get; set; }
        public decimal Saldo { get; set; }
        public string? Cor { get; set; }
        public decimal? Limite { get; set; }
        public int? DiaVencimento { get; set; }
        public int? DiaFechamento { get; set; }
    }

    
    public class CriarContaCommandHandler : IRequestHandler<CriarContaCommand, CriarContaResponse>
    {
        private readonly IContaRepository _contaRepository;

        public CriarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<CriarContaResponse> Handle(CriarContaCommand request, CancellationToken cancellationToken)
        {
            // Verifica se já existe conta com o mesmo nome para o usuário
         
            var nomeExiste = await _contaRepository.ExisteContaComNomeAsync(request.UsuarioId, request.Nome);
            if (nomeExiste)
                throw new ApplicationException("Já existe uma conta com esse nome para o usuário.");

            // Ajusta saldo se for devedor
            if (request.Saldo < 0)
                request.Saldo *= -1;

            // Se for cartão e DiaFechamento não informado, calcula
            if (request.Tipo == TipoConta.CartaoCredito)
            {
                if (!request.DiaVencimento.HasValue)
                    throw new ApplicationException("Dia de vencimento é obrigatório para cartão.");

                if (!request.Limite.HasValue)
                    throw new ApplicationException("Limite é obrigatório para cartão.");

                if (!request.DiaFechamento.HasValue)
                    request.DiaFechamento = request.DiaVencimento.Value - 10;
            }

            var conta = new Conta(
                id: request.UsuarioId,
                nome: request.Nome,
                tipo: request.Tipo,
                saldo: request.Saldo,
                cor: request.Cor,
                limite: request.Limite,
                diaVencimento: request.DiaVencimento
            );

            await _contaRepository.AdicionarAsync(conta);

            return new CriarContaResponse
            {
                ContaId = conta.Id,
                Nome = conta.Nome,
                Tipo = conta.Tipo,
                Saldo = conta.Saldo
            };

        }
    }
    public class CriarContaResponse
    {
        public Guid ContaId { get; set; }
        public string Nome { get; set; }
        public TipoConta Tipo { get; set; }
        public string? Cor { get; set; }
        public string Moeda { get; set; }
        public decimal Saldo { get; set; }
        public TipoLimite? Limite { get; set; }
        public decimal? LimiteDisponivel { get; set; } // Calculado para cartões
        public int? DiaVencimento { get; set; }
        public bool Ativa { get; set; }

        public void Deconstruct(out object mensagem, out object sucesso)
        {
            throw new NotImplementedException();
        }
    }

}
