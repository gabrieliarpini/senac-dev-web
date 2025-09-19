using MeuCorre.Domain.Enums;

namespace MeuCorre.Application.UseCases.Categorias.Dtos
{
    public record CategoriaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }
        public bool Ativo { get; set; }
        public TipoTransacao Tipo { get; set; }
        public DateTime? UltimaAlteracao { get; set; }
    }
}
