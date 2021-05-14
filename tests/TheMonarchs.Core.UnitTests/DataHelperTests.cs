using FluentAssertions;
using TheMonarchs.Core.Utilities;
using Xunit;
namespace TheMonarchs.Core.Tests
{
    public class DataHelperTests
    {
        [Fact]
        public void ExtractStartEndYear_NullInput_ReturnsNull()
        {
            //Arrange
            string input = null;

            //Act

            var result = DataHelper.ExtractStartEndYear(input);

            //Assert
            result.Should().BeNull();

        }


        [Fact]
        public void ExtractStartEndYear_EmptyInput_ReturnsNull()
        {
            //Arrange
            string input = string.Empty;

            //Act

            var result = DataHelper.ExtractStartEndYear(input);

            //Assert
            result.Should().BeNull();

        }


        [Fact]
        public void ExtractStartEndYear_NonIntegerInputOneYear_ReturnsNullBothYears()
        {
            //Arrange
            string input = "xxx";

            //Act

            var result = DataHelper.ExtractStartEndYear(input);

            //Assert
            result.Value.startYear.Should().BeNull();
            result.Value.endYear.Should().BeNull();
        }

        [Fact]
        public void ExtractStartEndYear_ValidInputOneYear_ReturnsStartYear()
        {
            //Arrange
            string input = "1920";
            (int? startYear, int? endYear)? expectedOutput = (1920, null);

            //Act

            var result = DataHelper.ExtractStartEndYear(input);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);

        }

        [Fact]
        public void ExtractStartEndYear_ValidInputBothYears_ReturnsStartEndYear()
        {
            //Arrange
            string input = "1920-1945";
            (int? startYear, int? endYear)? expectedOutput = (1920, 1945);

            //Act

            var result = DataHelper.ExtractStartEndYear(input);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);

        }



        [Fact]
        public void ExtractFirstLastName_NullInput_ReturnsNull()
        {
            //Arrange
            string input = null;

            //Act

            var result = DataHelper.ExtractFirstLastName(input);

            //Assert
            result.Should().BeNull();
        }


        [Fact]
        public void ExtractFirstLastName_EmptyInput_ReturnsNull()
        {
            //Arrange
            string input = string.Empty;

            //Act

            var result = DataHelper.ExtractFirstLastName(input);

            //Assert
            result.Should().BeNull();

        }


        [Fact]
        public void ExtractFirstLastName_ValidInputFirstName_ReturnsValidFirstName()
        {
            //Arrange
            string input = "Mohsin";
            (string firstName, string lastName)? expectedOutput = (input, null);

            //Act

            var result = DataHelper.ExtractFirstLastName(input);

            //Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }


        [Fact]
        public void ExtractFirstLastName_ValidInputFullName_ReturnsValidFirstName()
        {
            //Arrange
            string input = "Mohsin Mehmood";

            (string firstName, string lastName)? expectedOutput = ("Mohsin", "Mehmood");


            //Act

            var result = DataHelper.ExtractFirstLastName(input);

            //Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }
    }
}
