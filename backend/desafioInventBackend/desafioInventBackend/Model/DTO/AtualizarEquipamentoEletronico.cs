using System.ComponentModel.DataAnnotations;

namespace DesafioInventBackend.Model.DTO
{
    public class AtualizarEquipamentoEletronico
    {

        [Required]
        public string? Nome { get; set; }


    }
}
