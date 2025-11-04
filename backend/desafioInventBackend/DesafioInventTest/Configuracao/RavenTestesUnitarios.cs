using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.Embedded;
using Raven.TestDriver;

namespace DesafioInventTest.Configuracao
{
    public class RavenTestesUnitarios : RavenTestDriver
    {
        public Dictionary<string, IDocumentStore> storesDosBancos { get; private set; }
        public string UrlDoServidorDeBanco { get; private set; }

        public RavenTestesUnitarios()
        {
            ConfigureServer(new TestServerOptions
            {
                DataDirectory = "c:\\STUDYSPACE/RavenDir",
                Licensing = new ServerOptions.LicensingOptions
                {
                    License = "",
                    
                }
            });
            storesDosBancos = new Dictionary<string, IDocumentStore>();
        }

        public override void Dispose()
        {
            base.Dispose();
            DestruirBancoDeDadosTeste();
        }

        private void DestruirBancoDeDadosTeste()
        {
            foreach (var storeNomeado in this.storesDosBancos)
            {
                storeNomeado.Value.Dispose();
            }
        }

        public void SalvarAlteracoes(IDocumentSession session)
        {
            session.SaveChanges();
            WaitForIndexing(session.Advanced.DocumentStore);
            System.Threading.Thread.Sleep(100);
        }

        public void CancelarAlteracoes(IDocumentSession session)
        {
            session.Advanced.Clear();
        }

        public IDocumentStore ObterNovoStore(string nomeDoBanco)
        {
            var store = GetDocumentStore(database: nomeDoBanco);
            storesDosBancos.Add(nomeDoBanco, store);

            return store;
        }

        public void AdicionarListaComId<T>(IDocumentSession session, List<KeyValuePair<string, T>> lista) where T : new()
        {
            lista.ForEach(x => session.Store(x.Value, x.Key));
            SalvarAlteracoes(session);
        }

        protected override void PreInitialize(IDocumentStore documentStore)
        {
            documentStore.Conventions.MaxNumberOfRequestsPerSession = 100;
            documentStore.Conventions.UseOptimisticConcurrency = false;
            documentStore.Conventions.IdentityPartsSeparator = '-';
            documentStore.Conventions.SaveEnumsAsIntegers = true;
        }
    }
}
