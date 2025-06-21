using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Repository.Contract
{
    public interface IEquipamentoEletronicoRepository
    {
        Task<ICollection<EquipamentoEletronico>> listarEquipamentosEletronicos();

        Task<ICollection<EquipamentoEletronico>> buscarEquipamentoEletronicoPorId(int id);

        Task<EquipamentoEletronico> salvarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico);
    }
}
