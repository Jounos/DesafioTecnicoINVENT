using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioInventBackend.Model.Entity
{
    public class EquipamentoEletronico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Nome { get; set; }
        [Required]
        public TipoEquipamento TipoEquipamento { get; set; }
        [Required]
        public int QuantidadeEstoque { get; set; }
        [NotMapped]
        public bool TemEstoque { get; set; }
        [Required]
        public DateTime DataInclusao { get; set; }
        public DateTime? DataExclusao { get; set; }

        public EquipamentoEletronico()
        {
            TemEstoque = QuantidadeEstoque > 0;
        }
    }

    public enum TipoEquipamento
    {
        PC = 0,
        Notebook = 1,
        Mouse = 2,
        Teclado = 3
    }
}

