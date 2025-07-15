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

        public T GetById(int id)
        {
            T entity;
            var idDecodificado = HttpUtility.UrlDecode(id.ToString());
            using (IDocumentSession session = _store.OpenSession())
            {
                entity = session.Load<T>(idDecodificado);
            }

            return entity;
        }

        public void Insert(T entity)
        {
            using (IDocumentSession session = _store.OpenSession())
            {
                session.Store(entity);
                session.SaveChanges();
            }
        }

        public void Update(int id, T entity)
        {
            using (IDocumentSession session = _store.OpenSession())
            {
                T t = session.Load<T>(id.ToString());
                t = entity;
                session.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (IDocumentSession session = _store.OpenSession())
            {
                session.Delete(id.ToString());
                session.SaveChanges();
            }
        }
    }
}
