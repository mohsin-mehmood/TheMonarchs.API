using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using TheMonarchs.API.Common.Exceptions;
using TheMonarchs.API.MediatR.House.Commands;
using Xunit;
namespace TheMonarchs.API.Tests.Commands
{
    [Collection("QueriesTestCollection")]
    public class FindMonarchsByHouseCommandTests
    {
        IServiceCollection services;
        public FindMonarchsByHouseCommandTests(ServiceCollectionFixture servicesCollectionFixture)
        {

            services = servicesCollectionFixture.services;
        }

        [Fact]
        public async Task FindMonarchsByHouseCommand_NullInput_Exception()
        {

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                //Arrange
                var mediartR = scope.ServiceProvider.GetService<ISender>();


               await FluentActions.Invoking(() => mediartR.Send(new FindMonarchsByHouseCommand
                {
                    HouseName = null
                })).Should().ThrowAsync<ValidationException>();

            }
        }


        [Fact]
        public async Task FindMonarchsByHouseCommand_InvalidInput_NotFoundException()
        {

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                //Arrange
                var mediartR = scope.ServiceProvider.GetService<ISender>();


               await FluentActions.Invoking(() => mediartR.Send(new FindMonarchsByHouseCommand
                {
                    HouseName = "xxxx"
                })).Should().ThrowAsync<NotFoundException>();

            }
        }


        [Fact]
        public async Task FindMonarchsByHouseCommand_ValidInput_NotFoundException()
        {

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                //Arrange
                var mediartR = scope.ServiceProvider.GetService<ISender>();
                var houseName = "House of Plantagenet";


                var houseMonarchsList = await mediartR.Send(new FindMonarchsByHouseCommand
                {
                    HouseName = houseName
                });

                houseMonarchsList.Should().HaveCount(1);


               var houseMonarchs = houseMonarchsList.First();

                houseMonarchs.House.Should().Be(houseName);
                houseMonarchs.Monarchs.Should().HaveCount(3);
                houseMonarchs.YearsRuled.Should().Be(111);
            }
        }
    }
}
