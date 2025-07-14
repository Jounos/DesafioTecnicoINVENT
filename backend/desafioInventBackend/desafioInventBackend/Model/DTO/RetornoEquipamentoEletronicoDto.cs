using System.ComponentModel.DataAnnotations;

namespace DesafioInventBackend.Model.DTO
{
    public class RetornoEquipamentoEletronicoDto: EquipamentoEletronicoDto
    {

        public int Id { get; set; }
        
        public bool TemEstoque { get; set; }

        public DateTime? DataInclusao { get; set; }

        public DateTime? DataExclusao { get; set; }


        public RetornoEquipamentoEletronicoDto(): base() {
            this.TemEstoque = (this.QuantidadeEstoque > 0);
        }
    }
}
