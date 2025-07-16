using System.ComponentModel.DataAnnotations;

namespace DesafioInventBackend.Model.DTO
{
    public class RetornoEquipamentoEletronicoDTO: EquipamentoEletronicoDTO
    {

        public string Id { get; set; } = string.Empty;
        
        public bool TemEstoque { get { return this.QuantidadeEstoque > 0; } }

        public DateTime DataInclusao { get; set; }

        public DateTime? DataExclusao { get; set; }

        public RetornoEquipamentoEletronicoDTO(): base() {
            
        }
    }
}
