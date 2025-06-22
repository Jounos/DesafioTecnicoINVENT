using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Repository.Contract
{
    public interface IEquipamentoEletronicoRepository
    {
        public ICollection<EquipamentoEletronico> listarEquipamentosEletronicos();

        public EquipamentoEletronico buscarEquipamentoEletronicoPorId(int id);

        public bool cadastrarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico);

        public bool atualizarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico);
    }
}
