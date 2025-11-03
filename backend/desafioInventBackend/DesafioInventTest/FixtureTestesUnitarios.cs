using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace DesafioInventTest
{
    public class FixtureTestesUnitarios : IDisposable
    {

        public RavenDBTest RavenDBTest;

        public FixtureTestesUnitarios()
        {
            RavenDBTest = new RavenDBTest();
        }

        public void AdicionarListaComId<T>(IDocumentSession session, List<KeyValuePair<string, T>> lista) where T : new()
        {
            RavenDBTest.AdicionarListaComId(session, lista);
        }

        public void SalvarAlteracoes(IDocumentSession session)
        {
            RavenDBTest.SalvarAlteracoes(session);
        }

        public void CancelarAlteracoes(IDocumentSession session)
        {
            RavenDBTest.CancelarAlteracoes(session);
        }

        public IDocumentStore ObterNovoStore(IDocumentSession session)
        {
            return RavenDBTest.ObterNovoStore(session);
        }

        public void Dispose()
        {
            RavenDBTest.Dispose();
        }
    }
}
