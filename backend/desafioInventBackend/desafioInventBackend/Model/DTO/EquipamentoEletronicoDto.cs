using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Model.DTO
{
    public class EquipamentoEletronicoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public TipoEquipamento TipoEquipamento { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
