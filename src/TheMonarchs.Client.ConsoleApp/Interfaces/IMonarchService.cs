using System.Threading.Tasks;
using TheMonarchs.Core.Dto;

namespace TheMonarchs.Client.ConsoleApp.Interfaces
{
    public interface IMonarchService
    {
        Task<MonarchDto> GetLongestRulingMonarchAsync();
        Task<HouseMonarchsDto> GetLongestRulingHouseAsync();

        Task<long?> GetMonarchsCountAsync();

        Task<FirstNameOccurencesDto> GetMostCommonFirstNameAsync();

    }
}
