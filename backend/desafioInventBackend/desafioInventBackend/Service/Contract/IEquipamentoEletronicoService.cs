using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Service.Contract
{
    public interface IEquipamentoEletronicoService
    {
        public ICollection<EquipamentoEletronico> listarEquipamentosEletronicos();

        public EquipamentoEletronico buscarEquipamentoEletronicoPorId(int id);

        public EquipamentoEletronicoDto cadastrarEquipamentoEletronico(EquipamentoEletronicoDto equipamentoEletronico);
        
        public EquipamentoEletronicoDto editarEquipamentoEletronico(int id, EquipamentoEletronicoDto equipamentoEletronico);

        public bool excluirEquipamentoEletronico(int id);
    }
}
