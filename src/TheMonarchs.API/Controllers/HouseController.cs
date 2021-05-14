using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TheMonarchs.API.MediatR.House.Commands;
using TheMonarchs.API.MediatR.House.Queries;
using TheMonarchs.Core.Dto;

namespace TheMonarchs.API.Controllers
{
    /// <summary>
    /// Controller of House related operations
    /// </summary>
    [ApiVersion("1.0")]
    public class HouseController : ApiControllerBase
    {

        /// <summary>
        /// Finds Monarchs by house name.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("/monarchs/findbyname")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<HouseMonarchsDto>> FindMonarchsByHouse(FindMonarchsByHouseCommand query)
        {
            var houseMonarchs = await Mediator.Send(query);
            return Ok(houseMonarchs);
        }



        /// <summary>
        /// Get the longest ruling house
        /// </summary>
        /// <returns></returns>
        [Route("LongestRuling")]
        [HttpGet]
        public async Task<ActionResult<HouseMonarchsDto>> GetLongestRulingHouse()
        {

            var longestRulingHouse = await Mediator.Send(new GetLongestRulingHouseQuery());

            return Ok(longestRulingHouse);
        }

    }
}
