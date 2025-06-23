using System.ComponentModel.DataAnnotations;

namespace DesafioInventBackend.Model.DTO
{
    public class RetornoEquipamentoEletronicoDto: EquipamentoEletronicoDto
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime? DataInclusao { get; set; }

        public DateTime? DataExclusao { get; set; }

        public RetornoEquipamentoEletronicoDto(): base() { }
    }
}
