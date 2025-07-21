using DesafioInventBackend.Data;
using DesafioInventBackend.Model.Entity;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DesafioInventBackend.Repository
{
    public class RavenDbRepository : IRepositoryEquipamentoEletronico<EquipamentoEletronico>
    {

        private readonly IDocumentStore _store = RavenDbContext.Store;

        public IEnumerable<EquipamentoEletronico> ListarTodos()
        {
            using IDocumentSession session = _getOpenedSession();
            return session.Query<EquipamentoEletronico>().ToList();
        }

        public EquipamentoEletronico BuscarPorId(string id)
        {
            using IDocumentSession session = _getOpenedSession();
            return session.Load<EquipamentoEletronico>(id) ?? null;
        }

        public EquipamentoEletronico Cadastrar(EquipamentoEletronico entity)
        {

            entity.DataInclusao = DateTimeOffset.Now;

            using IDocumentSession session = _getOpenedSession();
            session.Store(entity);
            session.SaveChanges();
            return entity;
        }

        public EquipamentoEletronico Atualizar(string id, EquipamentoEletronico entity)
        {
            using IDocumentSession session = _getOpenedSession();

            EquipamentoEletronico equipamentoEletronico = session.Load<EquipamentoEletronico>(id);
            equipamentoEletronico.Nome = entity.Nome;
            equipamentoEletronico.TipoEquipamento = entity.TipoEquipamento;
            equipamentoEletronico.QuantidadeEstoque = entity.QuantidadeEstoque;

            session.SaveChanges();
            return equipamentoEletronico;
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
