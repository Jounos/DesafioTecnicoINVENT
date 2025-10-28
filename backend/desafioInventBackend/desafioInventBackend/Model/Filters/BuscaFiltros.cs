using DesafioInventBackend.Model.Enum

namespace DesafioInventBackend.Model.Filters
{
    public class BuscaFiltros
    {
        public string Nome { get; set; } = string.Empty;

        public TipoEquipamentoEnum TipoEquipamento { get; set; }

        public EquipamentoEmEstoqueEnum EquipamentoEmEstoqueEnum { get; set; }

        public DateTimeOffset DataInicio { get; set; }

        public DateTimeOffset DataFim { get; set; }

    }
}
