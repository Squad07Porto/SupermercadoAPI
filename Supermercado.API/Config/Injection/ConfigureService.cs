using Supermercado.API.Services;
using Supermercado.API.Services.Interfaces;

namespace Supermercado.API.Config.Injection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IFuncionarioService, FuncionarioService>();
            services.AddScoped<ISecaoService, SecaoService>();
            services.AddScoped<IProdutoService, ProdutoService>();
        }
    }
}