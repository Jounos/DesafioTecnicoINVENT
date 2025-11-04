using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DesafioInventTest.Configuracao
{
    public class FixtureTestesUnitarios : IDisposable
    {
        public RavenTestesUnitarios RavenTestesUnitarios;
        public FixtureTestesUnitarios()
        {
            RavenTestesUnitarios = new RavenTestesUnitarios();
        }

        public void AdicionarListaComId<T>(IDocumentSession session, List<KeyValuePair<string, T>> lista) where T : new()
        {
            RavenTestesUnitarios.AdicionarListaComId(session, lista);
        }

        public void SalvarAlteracoes(IDocumentSession session)
        {
            RavenTestesUnitarios.SalvarAlteracoes(session);
        }

        public void CancelarAlteracoes(IDocumentSession session)
        {
            RavenTestesUnitarios.CancelarAlteracoes(session);
        }

        public IDocumentStore ObterNovoStore(string db)
        {
            return RavenTestesUnitarios.ObterNovoStore(db);
        }

        public void Dispose()
        {
            RavenTestesUnitarios.Dispose();
        }
    }
}
