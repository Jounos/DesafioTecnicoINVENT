using System.ComponentModel.DataAnnotations;

namespace DesafioInventBackend.Model.DTO
{
    public class CriarEquipamentoEletronicoDto
    {

        [Required]
        public string? Nome { get; set; }

        [Required]
        public int tipoEquipamentoId { get; set; }

        [Required]
        public int QuantidadeEstoque { get; set; }
    }
}
