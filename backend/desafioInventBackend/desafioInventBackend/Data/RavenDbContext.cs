using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DesafioInventBackend.Data
{

    public class RavenDbContext : IServicoSessaoRaven
    {
        
        private static Lazy<IDocumentStore> _store = new Lazy<IDocumentStore>(CreateStore);
        
        public IDocumentSession Session { get; set; }
        public IDocumentStore Store { get; set; }

        public RavenDbContext() 
        {
            Store = _store.Value;
            if (Session is null)
            {
                Session = Store.OpenSession();
            }
        }

        private static IDocumentStore CreateStore()
        {
            IDocumentStore store = new DocumentStore()
                {
                    Urls = new[] { Environment.GetEnvironmentVariable("ravenDbServer") },

                    Database = Environment.GetEnvironmentVariable("ravenDbName"),
                }.Initialize();

            return store;
        }
    }
}
