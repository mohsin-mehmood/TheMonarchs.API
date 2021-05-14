using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TheMonarchs.Client.ConsoleApp.Interfaces;
using TheMonarchs.Client.ConsoleApp.Utility;
using TheMonarchs.Core.Dto;

namespace TheMonarchs.Client.ConsoleApp.Services
{
    public class MonarchService : IMonarchService
    {
        private readonly HttpClient _httpClient;

        public MonarchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MonarchDto> GetLongestRulingMonarchAsync()
        {
            var apiUrl = "Monarchs/LongestRuling";

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonDeserializer.Deserialize<MonarchDto>(jsonResponse);
            }


            return null;
        }


        public async Task<HouseMonarchsDto> GetLongestRulingHouseAsync()
        {
            var apiUrl = "House/LongestRuling";

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonDeserializer.Deserialize<HouseMonarchsDto>(jsonResponse);
            }


            return null;
        }

        public async Task<long?> GetMonarchsCountAsync()
        {
            var apiUrl = "Monarchs/All";

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode && response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> headerValues))
            {
                if (headerValues.Any())
                {
                    var pagingDetails = JsonDeserializer.Deserialize<PagingMetadata>(headerValues.First());

                    if (pagingDetails != null)
                    {
                        return pagingDetails.TotalCount;
                    }
                }
            }

            return null;

        }

        public async Task<FirstNameOccurencesDto> GetMostCommonFirstNameAsync()
        {
            var apiUrl = "Monarchs/MostCommonFirstName";

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonDeserializer.Deserialize<FirstNameOccurencesDto>(jsonResponse);
            }


            return null;
        }
    }
}
