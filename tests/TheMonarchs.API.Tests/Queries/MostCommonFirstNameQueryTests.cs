using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TheMonarchs.API.MediatR.Monarchs.Queries;
using Xunit;

namespace TheMonarchs.API.Tests.Queries
{
    [Collection("QueriesTestCollection")]
    public class MostCommonFirstNameQueryTests
    {
        IServiceCollection services;
        public MostCommonFirstNameQueryTests(ServiceCollectionFixture servicesCollectionFixture)
        {

            services = servicesCollectionFixture.services;
        }


        [Fact]
        public async Task MostCommonFirstNameQuery_Test()
        {
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                //Arrange
                var mediartR = scope.ServiceProvider.GetService<ISender>();

                //Act
                var result = await mediartR.Send(new MostCommonFirstNameQuery());


                //Assert
                result.Name.Should().Be("Edward");
                result.TotalFound.Should().Be(3);


            }
        }
    }
}
