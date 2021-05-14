using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMonarchs.Core.Dto;
using TheMonarchs.Core.Entities;
using TheMonarchs.Core.Interfaces;

namespace TheMonarchs.Infrastructure.Repositories
{
    public class MonarchDataRepository : IMonarchDataRepository
    {
        private readonly IQueryable<Monarch> _dataSource;

        public MonarchDataRepository(IMonarchDataProvider monarchDataProvider)
        {
            _dataSource = monarchDataProvider.MonarchesDataSource;
        }

        public async Task<PagedList<Monarch>> GetAllMonarchsAsync(PagingParams pagingParams)
        {

            IOrderedEnumerable<Monarch> query = _dataSource.AsEnumerable().OrderBy(m => m.YearStart);

            var result = PagedList<Monarch>.ToPagedList(query.AsQueryable(), pagingParams.PageNumber, pagingParams.PageSize);

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<(string house, IEnumerable<Monarch> Monarchs)>> GetMonarchsByHouseAsync(string houseName)
        {

            var houseMonarchs = _dataSource.Where(m => m.House.Contains(houseName, StringComparison.OrdinalIgnoreCase))
                                           .GroupBy(m => m.House)
                                           .AsEnumerable()
                                           .Select(x => (x.Key, x.Select(m => m)));
            return await Task.FromResult(houseMonarchs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<(string firstName, IEnumerable<string> occurences)> GetMostCommonFirstNameAsync()
        {
            var commonName = _dataSource.GroupBy(m => m.FirstName)
                                 .OrderByDescending(m => m.Count())
                                 .Take(1)
                                 .AsEnumerable()
                                 .Select(m => (m.Key, m.Select(x=>x.Name)));

            return await Task.FromResult(commonName.FirstOrDefault());
        }


        public async Task<Monarch> GetLongestRulingMonarchAsync()
        {
            var longestRulingMonarch = _dataSource.OrderByDescending(m => m.YearsRuled).FirstOrDefault();

            return await Task.FromResult(longestRulingMonarch);
        }

        public async Task<int> GetMonarchCountAsync()
        {
            var monarchCount = _dataSource.GroupBy(m => m.Name).Count();

            return await Task.FromResult(monarchCount);
        }

        public async Task<(string house, IEnumerable<Monarch> Monarchs)> GetLongestRulingHouseAsync()
        {
            var longestRulingHouse = _dataSource.GroupBy(m => m.House)
                                                           .OrderByDescending(m => (long)m.Sum(h => h.YearsRuled))
                                                           .Take(1)
                                                           .AsEnumerable()
                                                           .Select(x => (x.Key, x.Select(m => m)));

            return await Task.FromResult(longestRulingHouse.FirstOrDefault());
        }

         


        

    }
}
