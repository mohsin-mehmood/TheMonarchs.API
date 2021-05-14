using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TheMonarchs.API.MediatR.House.Queries;
using Xunit;

namespace TheMonarchs.API.Tests.Queries
{
    [Collection("QueriesTestCollection")]
    public class GetLongestRulingHouseQueryTests
    { 
        IServiceCollection services;
        public GetLongestRulingHouseQueryTests(ServiceCollectionFixture servicesCollectionFixture)
        { 

           services = servicesCollectionFixture.services;
        }

        
        [Fact]
        public async Task GetLongestRulingHouseQuery_Test()
        {
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                //Arrange
                var mediartR = scope.ServiceProvider.GetService<ISender>();

                //Act
                var result = await mediartR.Send(new GetLongestRulingHouseQuery());


                //Assert
                result.House.Should().Be("House of Plantagenet");
                result.YearsRuled.Should().Be(111);
            }
        }
 
    }
}
