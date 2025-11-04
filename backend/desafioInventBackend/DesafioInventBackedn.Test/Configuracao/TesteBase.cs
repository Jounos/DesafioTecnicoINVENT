using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace DesafioInventTest.Configuracao
{
    public abstract class TesteBase : IDisposable
    {
        protected IServiceCollection _services;
        protected ServiceProvider _serviceProvider;
        protected TesteBase()
        {
            DefinirCulturaPadrao();
            _services = ObterServiceCollectionComDependenciasDosProjetos();
            _serviceProvider = _services.BuildServiceProvider();
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

        protected IServiceCollection ObterServiceCollectionComDependenciasDosProjetos()
        {
            var services = new ServiceCollection();

            //services.AddScoped<ServicoDeServerMock>();

            return services;
        }

        public virtual void Dispose()
        {
        }
    }
}
