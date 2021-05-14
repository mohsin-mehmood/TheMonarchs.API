using System.Collections.Generic;
using System.Threading.Tasks;
using TheMonarchs.Core.Dto;
using TheMonarchs.Core.Entities;
namespace TheMonarchs.Core.Interfaces
{
    public interface IMonarchDataRepository
    {

        Task<PagedList<Monarch>> GetAllMonarchsAsync(PagingParams pagingParams);

        Task<IEnumerable<(string house, IEnumerable<Monarch> Monarchs)>> GetMonarchsByHouseAsync(string houseName);

        Task<(string firstName, IEnumerable<string> occurences)> GetMostCommonFirstNameAsync();

        Task<Monarch> GetLongestRulingMonarchAsync();

        Task<int> GetMonarchCountAsync();

        Task<(string house, IEnumerable<Monarch> Monarchs)> GetLongestRulingHouseAsync();

    }
}
