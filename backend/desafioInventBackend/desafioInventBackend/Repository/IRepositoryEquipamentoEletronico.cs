using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Filters;
using Raven.Client.Documents.Session;

namespace DesafioInventBackend.Repository
{
    public interface IRepositoryEquipamentoEletronico
    {
        IEnumerable<EquipamentoEletronico> Buscar(BuscaFiltros filtros = null);
        EquipamentoEletronico BuscarPorId(string id, IDocumentSession session = null);
        void Cadastrar(EquipamentoEletronico equipamentoEletronico);
        void Atualizar(string id, EquipamentoEletronico equipamentoEletronicoModificado);
        void Deletar(string id);
    }
}
