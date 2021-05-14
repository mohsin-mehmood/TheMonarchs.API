using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TheMonarchs.API.Automapper;
using TheMonarchs.Core.Dto;
using TheMonarchs.Core.Entities;
using Xunit;

namespace TheMonarchs.API.Tests.Mapper
{
    public class AutomapperTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public AutomapperTests()
        {

            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ValidConfiguration_Test()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(Monarch), typeof(MonarchDto))]
        [InlineData(typeof((string house, IEnumerable<Monarch> Monarchs)), typeof(HouseMonarchsDto))]
        [InlineData(typeof((string firstName, IEnumerable<string> occurences)), typeof(FirstNameOccurencesDto))]
        public void MappingFromSourceToDestination_IsSupported(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            var mappedObject = _mapper.Map(instance, source, destination);

            mappedObject.Should().NotBeNull();
        }


            private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type);

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }

    }
}
