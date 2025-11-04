using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Text.RegularExpressions;
using Xunit.Sdk;

namespace DesafioInventTest.Configuracao
{
    public abstract class BaseParaTesteUnitario : TesteBase
    {
        public FixtureTestesUnitarios _fixture;
        
        private readonly ITestContextAccessor _contextAccessor;
        public IDocumentSession _session { get; private set; }
        public IDocumentStore _store { get; private set; }

        protected BaseParaTesteUnitario(ITestContextAccessor contextAccessor) : base()
        {
            _contextAccessor = contextAccessor;

            _fixture = new FixtureTestesUnitarios(new RavenTestesUnitarios());

            var nome = ObterNomeDoTeste();

            _store = _fixture.ObterNovoStore(nome);
            _session = CriarNovaSessao();
        }

        protected IDocumentSession CriarNovaSessao()
        {
            if (_session != null)
            {
                _fixture.SalvarAlteracoes(_session);
                _session.Dispose();
            }

            _session = _store.OpenSession();
            return _session;
        }

        public override void Dispose()
        {
            base.Dispose();
            _session.Dispose();
            _store.Dispose();
            _fixture.Dispose();
        }

        public string ObterNomeDoTeste()
        {
            var test = _contextAccessor?.Current?.Test;
            var nomeDoTeste = test?.TestDisplayName?.Split('.').Last() ?? "TesteDesconhecido";
            return Regex.Replace(nomeDoTeste, "\\W", "_");

        }
    }
}
