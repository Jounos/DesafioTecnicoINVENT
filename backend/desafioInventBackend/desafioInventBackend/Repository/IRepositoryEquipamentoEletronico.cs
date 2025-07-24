using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Repository
{
    public interface IRepositoryEquipamentoEletronico
    {
        IEnumerable<EquipamentoEletronico> ListarTodos();
        EquipamentoEletronico BuscarPorId(string id);
        void Cadastrar(EquipamentoEletronico equipamentoEletronico);
        void Atualizar(string id, EquipamentoEletronico equipamentoEletronicoModificado);
        void Deletar(string id);
    }
}
