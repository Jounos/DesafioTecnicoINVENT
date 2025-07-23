using DesafioInventBackend.Model.DTO;

namespace DesafioInventBackend.Model.Entity
{
    public class EquipamentoEletronico
    {

        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public TipoEquipamento TipoEquipamento { get; set; }
        public int QuantidadeEstoque { get; set; }
        public DateTimeOffset  DataInclusao { get; set; }

        public EquipamentoEletronico()
        {

        }
    }

    public enum TipoEquipamento
    {
        PC = 1,
        Notebook = 2,
        Mouse = 3,
        Teclado = 4,
        Monitor = 5
    }
}

