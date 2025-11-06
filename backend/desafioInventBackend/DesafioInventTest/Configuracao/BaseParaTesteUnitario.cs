using DesafioInventBackend.Data;
using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository;
using DesafioInventBackend.Service;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Text.RegularExpressions;

namespace DesafioInventTest.Configuracao
{
    public abstract class BaseParaTesteUnitario : IDisposable
    {
        protected IServiceCollection _services;
        
        public FixtureTestesUnitarios _fixture;
        
        private readonly ITestContextAccessor _contextAccessor;
        public IDocumentSession _session { get; private set; }
        public IDocumentStore _store { get; private set; }

        protected BaseParaTesteUnitario(ITestContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _fixture = new FixtureTestesUnitarios(new RavenTestesUnitarios());
            _services = ObterServiceCollectionComDependencias();

            var nome = ObterNomeDoTeste();
            Environment.SetEnvironmentVariable("ravenDbName", nome);

            _store = _fixture.ObterNovoStore(nome);
            Environment.SetEnvironmentVariable("ravenDbServer", string.Join(";", _store.Urls));

            _session = CriarNovaSessao();

            _services.AddScoped<IServicoSessaoRaven>((provider) =>
                new RavenDbContext
                {
                    Store = _store,
                    Session = _session
                }
            );

            _services.BuildServiceProvider();
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

        public string ObterNomeDoTeste()
        {
            var test = _contextAccessor?.Current?.Test;
            var nomeDoTeste = test?.TestDisplayName?.Split('.').Last() ?? "TesteDesconhecido";
            return Regex.Replace(nomeDoTeste, "\\W", "_");

        }

        protected IServiceCollection ObterServiceCollectionComDependencias()
        {
            _services = new ServiceCollection();
            _services.AddSingleton<IServicoSessaoRaven, RavenDbContext>();
            _services.AddScoped<IRepositoryEquipamentoEletronico, RavenDbRepository>();
            _services.AddScoped<EquipamentoEletronicoCadastrarValidator>();
            _services.AddScoped<EquipamentoEletronicoAlterarValidator>();
            _services.AddScoped<EquipamentoEletronicoDeleteValidator>();
            _services.AddScoped<EquipamentoEletronicoService>();
            _services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<EquipamentoEletronico, EquipamentoEletronicoDTO>().ReverseMap();
            });

            return _services;
        }

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}
