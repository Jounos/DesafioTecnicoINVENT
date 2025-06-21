using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Service.Contract
{
    public interface IEquipamentoEletronicoService
    {
        Task<ICollection<EquipamentoEletronico>> listarEquipamentosEletronicos();

        Task<ICollection<EquipamentoEletronico>> buscarEquipamentoEletronicoPorId(int id);

        Task<EquipamentoEletronico> cadastrarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico);
        
        Task<EquipamentoEletronico> editarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico);

        Task<bool> excluirEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico);
    }
}
