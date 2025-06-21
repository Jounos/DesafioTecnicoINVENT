using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioInventBackend.Model
{
    public class EquipamentoEletronico
    {
        public int Id { get; set; }

        [Required]
        public string? Nome { get; set; }
        [Required]
        public TipoEquipamento TipoEquipamento { get; set; }
        [Required]
        public int QuantidadeEstoque { get; set; }
        public bool TemEstoque { get; set; }
        [Required]
        public DateTime DataInclusao { get; set; }
        public DateTime DataExclusao { get; set; }

        public EquipamentoEletronico()
        {
            this.TemEstoque = this.QuantidadeEstoque > 0;
        }
    }

    public enum TipoEquipamento
    {
        PC,
        Notebook,
        Mouse,
        Teclado
    }
}

