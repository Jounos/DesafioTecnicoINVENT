using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Service.Contract
{
    public interface IEquipamentoEletronicoService
    {
        public ICollection<EquipamentoEletronico> listarEquipamentosEletronicos();

        public EquipamentoEletronico buscarEquipamentoEletronicoPorId(int id);

        public EquipamentoEletronico cadastrarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico);
        
        public EquipamentoEletronico editarEquipamentoEletronico(int id, EquipamentoEletronico equipamentoEletronico);

        public bool excluirEquipamentoEletronico(int id);
    }
}
