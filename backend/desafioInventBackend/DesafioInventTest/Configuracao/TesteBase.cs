using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Model.Validator;
using DesafioInventBackend.Repository;
using DesafioInventBackend.Service;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using System.Globalization;

namespace DesafioInventTest.Configuracao
{
    public abstract class TesteBase : IDisposable
    {
        protected IServiceCollection _services;
        protected TesteBase()
        {
            _services = ObterServiceCollectionComDependencias();
            DefinirCulturaPadrao();
        }

        private void DefinirCulturaPadrao()
        {
            if (string.IsNullOrWhiteSpace(CultureInfo.CurrentUICulture.Name))
            {
                var cultureInfo = new CultureInfo("pt-BR");
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            }
        }

        protected IServiceCollection ObterServiceCollectionComDependencias()
        {
            _services = new ServiceCollection();
            _services.AddSingleton<IDocumentStore>();
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

        public virtual void Dispose()
        {
        }
    }
}
