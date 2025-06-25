using DesafioInventBackend.Model.DTO;
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
        public bool TemEstoque { get; set;  }
        [Required]
        public DateTime DataInclusao { get; set; }
        public DateTime? DataExclusao { get; set; }

        public EquipamentoEletronico()
        {

        }

        public EquipamentoEletronico(AtualizarEquipamentoEletronicoDto equipamentoEletronicoDto)
        {
            this.Id = equipamentoEletronicoDto.Id;
            this.Nome = equipamentoEletronicoDto.Nome;
            this.TipoEquipamento = (TipoEquipamento)equipamentoEletronicoDto.TipoEquipamento;
            this.QuantidadeEstoque = equipamentoEletronicoDto.QuantidadeEstoque;
            this.TemEstoque = (equipamentoEletronicoDto.QuantidadeEstoque > 0);
            this.DataInclusao = (equipamentoEletronicoDto.DataInclusao == null ? DateTime.MinValue : equipamentoEletronicoDto.DataInclusao!.Value);
        }

        public EquipamentoEletronico(EquipamentoEletronicoDto equipamentoEletronicoDto)
        {
            this.Nome = equipamentoEletronicoDto.Nome;
            this.TipoEquipamento = (TipoEquipamento)equipamentoEletronicoDto.TipoEquipamento;
            this.QuantidadeEstoque = equipamentoEletronicoDto.QuantidadeEstoque;
            this.TemEstoque = (equipamentoEletronicoDto.QuantidadeEstoque > 0);
            this.DataInclusao = DateTime.Now;
        }

        public EquipamentoEletronico(RetornoEquipamentoEletronicoDto equipamentoEletronicoDto)
        {
            this.Id = equipamentoEletronicoDto.Id;
            this.Nome = equipamentoEletronicoDto.Nome;
            this.TipoEquipamento = (TipoEquipamento)equipamentoEletronicoDto.TipoEquipamento;
            this.QuantidadeEstoque = equipamentoEletronicoDto.QuantidadeEstoque;
            this.TemEstoque = (equipamentoEletronicoDto.QuantidadeEstoque > 0);
            this.DataInclusao = (equipamentoEletronicoDto.DataInclusao == null ? DateTime.MinValue : equipamentoEletronicoDto.DataInclusao!.Value);
            this.DataExclusao = (equipamentoEletronicoDto.DataExclusao == null ? null : equipamentoEletronicoDto.DataExclusao!.Value);
        }
    }

    public enum TipoEquipamento
    {
        PC = 1,
        Notebook = 2,
        Mouse = 3,
        Teclado = 4
    }
}

