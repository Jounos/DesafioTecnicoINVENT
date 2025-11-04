using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Reflection;
using System.Text.RegularExpressions;
using Xunit.Abstractions;

namespace DesafioInventTest.Configuracao
{
    public abstract class BaseParaTesteUnitario : TesteBase
    {
        public FixtureTestesUnitarios _fixture;
        private readonly ITestOutputHelper _helper;
        public IDocumentSession _session { get; private set; }
        public IDocumentStore _store { get; private set; }

        protected BaseParaTesteUnitario(ITestOutputHelper helper) : base()
        {
            _helper = helper;
            _fixture = new FixtureTestesUnitarios();

            var nome = ObterNomeDoTeste();

            _store = _fixture.ObterNovoStore(nome);
            _session = CriarNovaSessao();

            //VariaveisDeAmbienteParaTeste.AdicionarUrlDoServeDeBanco(_store.Urls.First());
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
            var valueHelper = _helper
                .GetType()
                ?.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(_helper) ?? throw new Exception("Não foi possivel encontrar a definição para o teste");

            var nomeDoTeste = ((ITest)valueHelper)
                .DisplayName
                .Split(".")
                .LastOrDefault() ?? throw new Exception("Não foi possivel encontrar a nome do teste");

            return Regex.Replace(nomeDoTeste, "\\W", "_");
        }
    }
}
