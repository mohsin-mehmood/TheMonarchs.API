using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TheMonarchs.API.MediatR.Monarchs.Queries;
using TheMonarchs.Core.Dto;

namespace TheMonarchs.API.Controllers
{
    /// <summary>
    /// Controller for Monarchs relation operations.
    /// </summary>
    [ApiVersion("1.0")]
    public class MonarchsController : ApiControllerBase
    {

        /// <summary>
        ///  Get paginated list of Monarchs. Pagination metadata returned as X-Pagination response header
        /// </summary>
        /// <param name="pagingParams">Page Number and Page size</param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<MonarchDto>>> All([FromQuery] GetMonarchsWithPaginationQuery pagingParams)
        {
            var response = await Mediator.Send(pagingParams);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(response.pageMetadata));

            return Ok(response.monarches);
        }

        /// <summary>
        /// Get the longest ruling Monarch details
        /// </summary>
        /// <returns></returns>
        [Route("LongestRuling")]
        [HttpGet]
        public async Task<ActionResult<MonarchDto>> GetLongestRulingMonarch()
        {
            return Ok(await Mediator.Send(new GetLongestRulingMonarchQuery()));
        }

        /// <summary>
        /// Gets the name of most common first name with number of occurrences.
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<FirstNameOccurencesDto>> MostCommonFirstName()
        {
            var commonFirstName = await Mediator.Send(new MostCommonFirstNameQuery());
            return Ok(commonFirstName);
        }
    }
}
