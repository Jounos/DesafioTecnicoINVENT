using Raven.Client.Documents;

namespace DesafioInventBackend.Data
{

    public class RavenDbContext
    {

        private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        private static IDocumentStore Store => store.Value;

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
