using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Model.DTO
{
    public class EquipamentoEletronicoDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public TipoEquipamento TipoEquipamento { get; set; }
        public int QuantidadeEstoque { get; set; }
        public DateTimeOffset DataInclusao { get; set; }

        public bool TemEstoque { get { return QuantidadeEstoque > 0; } }
    }
}
