using System.ComponentModel.DataAnnotations;

namespace DesafioInventBackend.Model.DTO
{
    public class AtualizarEquipamentoEletronicoDto : EquipamentoEletronicoDto
    {

        [Required]
        public int Id { get; set; }

        public DateTime? DataInclusao { get; set; }

        public AtualizarEquipamentoEletronicoDto(): base() {
        }
    }
}
