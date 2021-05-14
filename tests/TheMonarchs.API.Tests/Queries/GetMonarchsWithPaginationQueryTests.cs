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
    public class GetMonarchsWithPaginationQueryTests
    {
        IServiceCollection services;

        public GetMonarchsWithPaginationQueryTests(ServiceCollectionFixture servicesCollectionFixture)
        {
            services = servicesCollectionFixture.services;
        }


        [Fact]
        public async Task GetMonarchsWithPaginationQuery_InvalidPagingParams()
        {
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                //Arrange
                var mediartR = scope.ServiceProvider.GetService<ISender>();

                var pageNumber = 20;
                var pageSize = 10;

                var expectedPageMetaData = new PagingMetadata
                {
                    TotalCount = 14,
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    HasNext = false,
                    HasPrevious = true,
                    TotalPages = 2
                };

                //Act
                var result = await mediartR.Send(new GetMonarchsWithPaginationQuery
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });


                //Assert
                result.monarches.Should().BeEmpty();
                result.pageMetadata.Should().BeEquivalentTo(expectedPageMetaData);
            }
        }

        [Fact]
        public async Task GetMonarchsWithPaginationQuery_ValidPagingParams()
        {
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                //Arrange
                var mediartR = scope.ServiceProvider.GetService<ISender>();

                var pageNumber = 2;
                var pageSize = 5;

                var expectedPageMetaData = new PagingMetadata
                {
                    TotalCount = 14,
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    HasNext = true,
                    HasPrevious = true,
                    TotalPages = 3
                };

                //Act
                var result = await mediartR.Send(new GetMonarchsWithPaginationQuery
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });


                //Assert

                result.monarches.Should().HaveCount(5);
                result.pageMetadata.Should().BeEquivalentTo(expectedPageMetaData);
            }
        }

    }
}
