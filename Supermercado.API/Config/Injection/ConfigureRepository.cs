using Supermercado.API.Infrastructure.Repositories;
using Supermercado.API.Infrastructure.Repositories.Interfaces;

namespace Supermercado.API.Config.Injection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<ISecaoRepository, SecaoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFilialRepository, FilialRepository>();
            services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}