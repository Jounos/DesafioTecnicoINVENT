using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Service.Contract
{
    public interface IEquipamentoEletronicoService
    {
        public Task<ICollection<EquipamentoEletronico>> listarEquipamentosEletronicos();

        public Task<EquipamentoEletronico> buscarEquipamentoEletronicoPorId(int id);

        public Task<EquipamentoEletronico> cadastrarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico);
        
        public Task<EquipamentoEletronico> atualizarEquipamentoEletronico(int id, EquipamentoEletronico equipamentoEletronico);

        public Task<EquipamentoEletronico> excluirEquipamentoEletronico(int id);
    }
}
