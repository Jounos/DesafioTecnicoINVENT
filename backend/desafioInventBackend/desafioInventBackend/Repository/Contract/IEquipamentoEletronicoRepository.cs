using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Repository.Contract
{
    public interface IEquipamentoEletronicoRepository
    {
        public Task<ICollection<EquipamentoEletronico>> listarEquipamentosEletronicos();

        public Task<EquipamentoEletronico> buscarEquipamentoEletronicoPorId(int id);

        public Task<EquipamentoEletronico> cadastrarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico);

        public Task<EquipamentoEletronico> atualizarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico);
    }
}
