using System.ComponentModel.DataAnnotations;

namespace DesafioInventBackend.Model.DTO
{
    public class EquipamentoEletronicoDto
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int TipoEquipamentoId { get; set; }

        [Required]
        public int QuantidadeEstoque { get; set; }

        //[Required]
        //public DateTime DataInclusao { get; set; }
    }
}
