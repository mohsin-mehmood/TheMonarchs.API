using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TheMonarchs.API.MediatR.Monarchs.Queries;
using TheMonarchs.Core.Dto;
using Xunit;

namespace TheMonarchs.API.Tests.Queries
{
    [Collection("QueriesTestCollection")]
    public class GetLongestRulingMonarchQueryTests
    {

        IServiceCollection services;
        public GetLongestRulingMonarchQueryTests(ServiceCollectionFixture servicesCollectionFixture)
        {

            services = servicesCollectionFixture.services;
        }

        [Fact]
        public async Task GetLongestRulingMonarchQuery_Test()
        {
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                //Arrange
                var mediartR = scope.ServiceProvider.GetService<ISender>();
                var expectedResult = new MonarchDto
                {
                    Id = 22,
                    Name = "Henry III",
                    Country = "United Kingdom",
                    House= "House of Plantagenet",
                    RulingPeriod = "56 Years(1216 - 1272)",
                    YearsRuled =56
                };


                //Act
                var result = await mediartR.Send(new GetLongestRulingMonarchQuery());


                //Assert
                result.Should().BeEquivalentTo(expectedResult);
            }
        }

    }
}
