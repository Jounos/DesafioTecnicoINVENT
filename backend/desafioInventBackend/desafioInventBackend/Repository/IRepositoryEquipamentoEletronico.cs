using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Filters;

namespace DesafioInventBackend.Repository
{
    public interface IRepositoryEquipamentoEletronico
    {
        IEnumerable<EquipamentoEletronico> Buscar(BuscaFiltros filtros = null);
        EquipamentoEletronico BuscarPorId(string id);
        void Cadastrar(EquipamentoEletronico equipamentoEletronico);
        void Atualizar(string id, EquipamentoEletronico equipamentoEletronicoModificado);
        void Deletar(string id);
    }
}
