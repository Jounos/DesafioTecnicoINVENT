using DesafioInventBackend.Data;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Web;

namespace DesafioInventBackend.Repository
{
    public class RavenDbRepository<T>: IRepositoryEquipamentoEletronico<T> where T : class
    {

        private readonly IDocumentStore _store = RavenDbContext.Store;

        public IEnumerable<T> GetAll()
        {
            List<T> list = new List<T>();
            using (IDocumentSession session = _store.OpenSession())
            {
                list = session.Query<T>().ToList();
            }

            return list;
        }

        public T GetById(string id)
        {
            T entity;
            var idDecodificado = HttpUtility.UrlDecode(id.ToString());
            using (IDocumentSession session = _store.OpenSession())
            {
                entity = session.Load<T>(idDecodificado);
            }

            return entity;
        }

        public T Insert(T entity)
        {
            using (IDocumentSession session = _store.OpenSession())
            {
                session.Store(entity);
                session.SaveChanges();
            }
            return entity;
        }

        public T Update(string id, T entity)
        {
            var idDecodificado = HttpUtility.UrlDecode(id.ToString());
            using (IDocumentSession session = _store.OpenSession())
            {
                session.Store(entity, idDecodificado);
                session.SaveChanges();
            }
            return entity;
        }

        public void Delete(string id)
        {
            using (IDocumentSession session = _store.OpenSession())
            {
                session.Delete(id.ToString());
                session.SaveChanges();
            }
        }
    }
}
