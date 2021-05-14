using Microsoft.Extensions.DependencyInjection;
using System;
using TheMonarchs.API.Extensions;
using TheMonarchs.API.Tests.Mocked;
using Xunit;

namespace TheMonarchs.API.Tests
{
    public class ServiceCollectionFixture : IDisposable
    {
        private readonly IServiceCollection _services;

        public ServiceCollectionFixture()
        {
            _services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

            _services.AddLogging();
            _services.AddAppServicesRegistration();
            _services.AddRepositoriesRegistrations(new MonarchJsonDataProvider());
        }


        public IServiceCollection services => _services;

        public void Dispose()
        {
            _services.Clear();
        }
    }

    [CollectionDefinition("QueriesTestCollection")]
    public class ServiceCollectionFixtureCollection : ICollectionFixture<ServiceCollectionFixture>
    {

    }

}
