using System.ComponentModel.DataAnnotations;

namespace DesafioInventBackend.Model.DTO
{
    public class EquipamentoEletronicoDto
    {
              
        public string? Nome { get; set; }

        public int TipoEquipamento { get; set; }

        public int QuantidadeEstoque { get; set; }

        public EquipamentoEletronicoDto()
        {

        }
    }
}
