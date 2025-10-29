using DesafioInventBackend.Data;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Enum;
using DesafioInventBackend.Model.Filters;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace DesafioInventBackend.Repository
{
    public class RavenDbRepository : IRepositoryEquipamentoEletronico
    {

        private readonly IDocumentStore _store = RavenDbContext.Store;

        public IEnumerable<EquipamentoEletronico> BuscarPorFiltros(BuscaFiltros filtros)
        {
            using IDocumentSession session = _getOpenedSession();

            IRavenQueryable<EquipamentoEletronico> equipamentoEletronicoQuery = session.Query<EquipamentoEletronico>();
            
            if (filtros.Nome != string.Empty)
            {
                equipamentoEletronicoQuery = equipamentoEletronicoQuery.Search(ee => ee.Nome, filtros.Nome);
            }
            
            if (filtros.TipoEquipamento != 0)
            {
                equipamentoEletronicoQuery = equipamentoEletronicoQuery.Where(ee => ee.TipoEquipamento == filtros.TipoEquipamento);
            }
            
            if (filtros.DataInicio != DateTimeOffset.MinValue)
            {
                equipamentoEletronicoQuery = equipamentoEletronicoQuery.Where(ee => ee.DataInclusao >= filtros.DataInicio);
            }
            
            if (filtros.DataFim != DateTimeOffset.MinValue)
            {
                equipamentoEletronicoQuery = equipamentoEletronicoQuery.Where(ee => ee.DataInclusao <= filtros.DataFim);
            }

            if (filtros.EquipamentoEmEstoque != EquipamentoEmEstoqueEnum.TODOS)
            {
                if (filtros.EquipamentoEmEstoque == EquipamentoEmEstoqueEnum.EM_ESTOQUE)
                {
                    equipamentoEletronicoQuery = equipamentoEletronicoQuery.Where(ee => ee.QuantidadeEstoque > 0);
                }

                if (filtros.EquipamentoEmEstoque == EquipamentoEmEstoqueEnum.NAO_TEM_ESTOQUE)
                {
                    equipamentoEletronicoQuery = equipamentoEletronicoQuery.Where(ee => ee.QuantidadeEstoque == 0);
                }
            }

            return equipamentoEletronicoQuery.OrderByDescending(ee => ee.DataInclusao).ToList();
        }

        public IEnumerable<EquipamentoEletronico> ListarTodos()
        {
            using IDocumentSession session = _getOpenedSession();
            return session.Query<EquipamentoEletronico>().OrderByDescending(ee => ee.DataInclusao).ToList();
        }

        public EquipamentoEletronico BuscarPorId(string id, IDocumentSession sessionOpened = null)
        { 
            sessionOpened ??= _getOpenedSession();
            return sessionOpened.Load<EquipamentoEletronico>(id) ?? throw new FormatException($"Não foi possível encontrar um equipamento eletrônico com id {id}");
        }

        public void Cadastrar(EquipamentoEletronico equipamentoEletronico)
        {
            equipamentoEletronico.DataInclusao = DateTimeOffset.Now;

            using IDocumentSession session = _getOpenedSession();
            session.Store(equipamentoEletronico);
            session.SaveChanges();
        }

        public void Atualizar(string id, EquipamentoEletronico equipamentoEletronicoModificado)
        {
            using IDocumentSession session = _getOpenedSession();
            
            EquipamentoEletronico equipamentoEletronico = BuscarPorId(id, session);

            equipamentoEletronico.Nome = equipamentoEletronicoModificado.Nome;
            equipamentoEletronico.TipoEquipamento = equipamentoEletronicoModificado.TipoEquipamento;
            equipamentoEletronico.QuantidadeEstoque = equipamentoEletronicoModificado.QuantidadeEstoque;
            
            session.SaveChanges();
        }

        public void Deletar(string id)
        {
            using IDocumentSession session = _getOpenedSession();
            session.Delete(id);
            session.SaveChanges();
        }

        private IDocumentSession _getOpenedSession()
        {
            return _store.OpenSession();
        }
    }
}
