using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace DesafioInventTest.Configuracao
{
    public class FixtureTestesUnitarios : IDisposable
    {
        private RavenTestesUnitarios _ravenTestesUnitarios;
        public FixtureTestesUnitarios(RavenTestesUnitarios ravenTestesUnitarios)
        {
            _ravenTestesUnitarios = ravenTestesUnitarios;
        }

        public void AdicionarListaComId<T>(IDocumentSession session, List<KeyValuePair<string, T>> lista) where T : new()
        {
            _ravenTestesUnitarios.AdicionarListaComId(session, lista);
        }

        public void SalvarAlteracoes(IDocumentSession session)
        {
            _ravenTestesUnitarios.SalvarAlteracoes(session);
        }

        public void CancelarAlteracoes(IDocumentSession session)
        {
            _ravenTestesUnitarios.CancelarAlteracoes(session);
        }

        public IDocumentStore ObterNovoStore(string db)
        {
            return _ravenTestesUnitarios.ObterNovoStore(db);
        }

        public void Dispose()
        {
            _ravenTestesUnitarios.Dispose();
        }
    }
}
