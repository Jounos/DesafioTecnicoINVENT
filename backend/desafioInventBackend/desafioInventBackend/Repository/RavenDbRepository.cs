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

        private readonly IDocumentStore _store;

        public RavenDbRepository(IDocumentStore store)
        {
            _store = store;
        }

        public IEnumerable<EquipamentoEletronico> Buscar(BuscaFiltros filtros = null)
        {
            using IDocumentSession session = _obterSessaoAberta();
            IRavenQueryable<EquipamentoEletronico> equipamentoEletronicoQuery = session.Query<EquipamentoEletronico>();

            if (filtros != null)
            {
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

        public EquipamentoEletronico BuscarPorId(string id, IDocumentSession sessionOpened = null)
        { 
            sessionOpened ??= _obterSessaoAberta();
            return sessionOpened.Load<EquipamentoEletronico>(id) ?? throw new FormatException($"Não foi possível encontrar um equipamento eletrônico com id {id}");
        }

        public void Cadastrar(EquipamentoEletronico equipamentoEletronico)
        {
            equipamentoEletronico.DataInclusao = DateTimeOffset.Now;

            using IDocumentSession session = _obterSessaoAberta();
            session.Store(equipamentoEletronico);
            session.SaveChanges();
        }

        public void Atualizar(string id, EquipamentoEletronico equipamentoEletronicoModificado)
        {
            using IDocumentSession session = _obterSessaoAberta();
            
            EquipamentoEletronico equipamentoEletronico = BuscarPorId(id, session);

            equipamentoEletronico.Nome = equipamentoEletronicoModificado.Nome;
            equipamentoEletronico.TipoEquipamento = equipamentoEletronicoModificado.TipoEquipamento;
            equipamentoEletronico.QuantidadeEstoque = equipamentoEletronicoModificado.QuantidadeEstoque;
            
            session.SaveChanges();
        }

        public void Deletar(string id)
        {
            using IDocumentSession session = _obterSessaoAberta();
            session.Delete(id);
            session.SaveChanges();
        }

        private IDocumentSession _obterSessaoAberta()
        {
            return _store.OpenSession();
        }
    }
}
