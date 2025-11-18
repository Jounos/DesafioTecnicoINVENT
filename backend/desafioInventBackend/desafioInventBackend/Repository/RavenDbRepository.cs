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

        private readonly IDocumentSession _session;

        public RavenDbRepository(IDocumentSession session)
        {
            _session = session;
        }

        public IEnumerable<EquipamentoEletronico> Buscar(BuscaFiltros filtros = null)
        {
            IRavenQueryable<EquipamentoEletronico> equipamentoEletronicoQuery = _session.Query<EquipamentoEletronico>();

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

        public EquipamentoEletronico BuscarPorId(string id)
        {
            return _session.Load<EquipamentoEletronico>(id) ?? throw new KeyNotFoundException($"Não foi possível encontrar um equipamento eletrônico com id {id}");
        }

        public void Cadastrar(EquipamentoEletronico equipamentoEletronico)
        {
            equipamentoEletronico.DataInclusao = DateTimeOffset.Now;

            _session.Store(equipamentoEletronico);
            _session.SaveChanges();
        }

        public void Atualizar(string id, EquipamentoEletronico equipamentoEletronicoModificado)
        {            
            EquipamentoEletronico equipamentoEletronico = BuscarPorId(id);

            equipamentoEletronico.Nome = equipamentoEletronicoModificado.Nome;
            equipamentoEletronico.TipoEquipamento = equipamentoEletronicoModificado.TipoEquipamento;
            equipamentoEletronico.QuantidadeEstoque = equipamentoEletronicoModificado.QuantidadeEstoque;

            _session.SaveChanges();
        }

        public void Deletar(string id)
        {
            _session.Delete(id);
            _session.SaveChanges();
        }

    }
}
