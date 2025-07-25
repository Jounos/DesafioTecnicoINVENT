using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Enum;

namespace DesafioInventBackend.Model.Entity
{
    public class EquipamentoEletronico
    {

        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public TipoEquipamentoEnum TipoEquipamento { get; set; }
        public int QuantidadeEstoque { get; set; }
        public DateTimeOffset  DataInclusao { get; set; }

        public EquipamentoEletronico()
        {

        }
    }
}

