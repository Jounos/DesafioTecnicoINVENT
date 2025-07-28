using DesafioInventBackend.Model.Entity;
using Raven.Client.Documents.Session;

namespace DesafioInventBackend.Repository
{
    public interface IRepositoryEquipamentoEletronico
    {
        IEnumerable<EquipamentoEletronico> ListarTodos();
        EquipamentoEletronico BuscarPorId(string id, IDocumentSession session = null);
        void Cadastrar(EquipamentoEletronico equipamentoEletronico);
        void Atualizar(string id, EquipamentoEletronico equipamentoEletronicoModificado);
        void Deletar(string id);
    }
}
