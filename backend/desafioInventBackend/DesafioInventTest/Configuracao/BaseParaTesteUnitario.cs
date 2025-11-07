using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository;
using DesafioInventBackend.Service;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Text.RegularExpressions;

namespace DesafioInventTest.Configuracao
{
    public abstract class BaseParaTesteUnitario : IDisposable
    {
        protected IServiceCollection _services;
        protected IServiceProvider _serviceProvider;
        
        public FixtureTestesUnitarios _fixture;
        public IDocumentStore _store;

        protected BaseParaTesteUnitario(ITestContextAccessor contextAccessor)
        {
            _fixture = new FixtureTestesUnitarios(new RavenTestesUnitarios());

            var nome = ObterNomeDoTeste(contextAccessor);
            Environment.SetEnvironmentVariable("ravenDbName", nome);

            _store = _fixture.ObterNovoStore(nome);
            Environment.SetEnvironmentVariable("ravenDbServer", string.Join(";", _store.Urls));

            _services = new ServiceCollection();

            _services.AddSingleton<IDocumentStore>(_store);

            _services.AddScoped<IDocumentSession>(provider =>
            {
                var store = provider.GetRequiredService<IDocumentStore>();
                return store.OpenSession();
            });

            _services.AddScoped<IRepositoryEquipamentoEletronico, RavenDbRepository>();
            _services.AddScoped<EquipamentoEletronicoCadastrarValidator>();
            _services.AddScoped<EquipamentoEletronicoAlterarValidator>();
            _services.AddScoped<EquipamentoEletronicoDeleteValidator>();
            _services.AddScoped<EquipamentoEletronicoService>();

            _services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<EquipamentoEletronico, EquipamentoEletronicoDTO>().ReverseMap();
            });

            _serviceProvider = _services.BuildServiceProvider();
        }

        protected IServiceScope CriarEscopo()
        {
            return _serviceProvider.CreateScope();
        }


        public string ObterNomeDoTeste(ITestContextAccessor contextAccessor)
        {
            var test = contextAccessor?.Current?.Test;
            var nomeDoTeste = test?.TestDisplayName?.Split('.').Last() ?? "TesteDesconhecido";
            return Regex.Replace(nomeDoTeste, "\\W", "_");

        }
        public void Dispose()
        {
            _store.Dispose();
            (_serviceProvider as IDisposable)?.Dispose();
        }

    }
}
