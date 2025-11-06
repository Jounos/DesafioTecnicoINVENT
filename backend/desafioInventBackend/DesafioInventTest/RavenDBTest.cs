using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.TestDriver;

namespace DesafioInventTest
{
    public class RavenDBTest : RavenTestDriver
    {

        public Dictionary<string, IDocumentStore> storesDosBancos { get; private set; }  
        
        public RavenDBTest()
        {
            storesDosBancos = new Dictionary<string, IDocumentStore>();
        }

        protected override void PreInitialize(IDocumentStore documentStore)
        {
            base.PreInitialize(documentStore);
            documentStore.Conventions.MaxNumberOfRequestsPerSession = 10;
            documentStore.Conventions.UseOptimisticConcurrency = false;
            documentStore.Conventions.IdentityPartsSeparator = '-';
            documentStore.Conventions.SaveEnumsAsIntegers = true;
        }

        public override void Dispose()
        {
            base.Dispose();
            _destruirBancoDeDadosTest();
        }

        private void _destruirBancoDeDadosTest()
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

        public IDocumentSession ObterNovoStore(string nomeDoBanco)
        {
            var store = GetDocumentStore(database: nomeDoBanco);
            storesDosBancos.Add(nomeDoBanco, store);

            return (IDocumentSession)store; 
        }

        public void AdicionarListaComId<T>(IDocumentSession session, List<KeyValuePair<string, T>> lista) where T : new()
        {
            lista.ForEach(x => session.Store(x.Value, x.Key));
            SalvarAlteracoes(session);
        }
    }
}
