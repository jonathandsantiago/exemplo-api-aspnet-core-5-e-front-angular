using FavoDeMel.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FavoDeMel.Api.Providers.Interface
{
    public interface IApiProvider
    {
        void AddProvider(IServiceCollection services, ISettings<string, object> settings);
    }
}
