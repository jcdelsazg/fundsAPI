using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funds_api.Extensions;
using funds_api.Implementation.Models;
using funds_api.Implementation.Resources;
using funds_api.Implementation.Services;

namespace funds_api.Implementation.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    [ApiController]
    public class FundsController : ControllerBase
    {
        private readonly IFundsService _fundsService;
        private readonly IMapper _mapper;

        public FundsController(IFundsService fundsService, IMapper mapper)
        {
            _fundsService = fundsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all the funds with his values
        /// </summary>
        /// <returns>List of funds with values</returns>
        /// <response code="200">Returns the list of funds</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IEnumerable<FundsResource>> GetAllAsync()
        {
            var funds = await _fundsService.ListAsync();
            var fundsResource = _mapper.Map<IEnumerable<Fund>, IEnumerable<FundsResource>>(funds);
            return fundsResource;
        }

        /// <summary>
        /// Find a found by Id
        /// </summary>
        /// <returns>Fund with values</returns>
        /// <response code="200">Returns fund with values</response>
        /// <response code="404">Return not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<FundsResource>> FindAsync(long id)
        {
            var result = await _fundsService.FindAsync(id);

            if (!result.Success)
                return NotFound(result.Message);

            var fundResult = _mapper.Map<Fund, FundsResource>(result.Funds);
            return Ok(fundResult);
        }

        /// <summary>
        /// Add a new fund
        /// </summary>
        /// <param name="fundResource"></param>
        /// <returns>The new fund created</returns>
        /// <response code="200">Returns the new fund created</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<FundsResource>> PostAsync([FromBody]SaveFundsResource fundResource)
        {
            var fund = _mapper.Map<SaveFundsResource, Fund>(fundResource);

            var result = await _fundsService.SaveAsync(fund);

            if (!result.Success)
                return BadRequest(result.Message);

            var fundResult = _mapper.Map<Fund, FundsResource>(result.Funds);

            return Ok(fundResult);
        }

        /// <summary>
        /// Uodate a fund
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fundResource"></param>
        /// <returns>The fund updated</returns>
        /// <response code="200">The fund updated</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Fund not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<FundsResource>> PutAsync(long id, [FromBody] SaveFundsResource fundResource)
        {
            var existingFund = await _fundsService.FindAsync(id);

            if (!existingFund.Success)
                return NotFound(existingFund.Message);

            var fund = _mapper.Map<SaveFundsResource, Fund>(fundResource);

            var result = await _fundsService.UpdateAsync(fund);

            if (!result.Success)
                return BadRequest(result.Message);

            var fundResult = _mapper.Map<Fund, FundsResource>(result.Funds);

            return Ok(fundResult);
        }
        
        /// <summary>
        /// Delete a fund
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Fund deleted</returns>
        /// <response code="200">Fund deleted</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Fund not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<FundsResource>> DeleteAsync(long id)
        {
            var existingFund = await _fundsService.FindAsync(id);

            if (!existingFund.Success)
                return NotFound(existingFund.Message);

            var result = await _fundsService.DeleteAsync(existingFund.Funds);

            if (!result.Success)
                return BadRequest(result.Message);

            var fundResource = _mapper.Map<Fund, FundsResource>(result.Funds);
            return Ok(fundResource);
        }
    }
}
