using System.ComponentModel.DataAnnotations;

namespace DesafioInventBackend.Model.DTO
{
    public class EquipamentoEletronicoDto
    {
        
        [Required]
        public string? Nome { get; set; }

        [Required]
        public int TipoEquipamento { get; set; }

        [Required]
        public int QuantidadeEstoque { get; set; }

        public EquipamentoEletronicoDto()
        {

        }
    }
}
