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
    public class ValueFundController : ControllerBase
    {
        private readonly IValueFundService _valueFundService;
        private readonly IMapper _mapper;
        private readonly IFundsService _fundService;

        public ValueFundController(IValueFundService valueFundService, IMapper mapper, IFundsService fundService)
        {
            _valueFundService = valueFundService;
            _mapper = mapper;
            _fundService = fundService;
        }

        /// <summary>
        /// Get all the valuesfunds 
        /// </summary>
        /// <returns>List of valuesfunds</returns>
        /// <response code="200">Returns the list of funds</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IEnumerable<ValueFundResource>> GetAllAsync()
        {
            var valueFunds = await _valueFundService.ListAsync();
            var valueFundsResource = _mapper.Map<IEnumerable<ValueFund>, IEnumerable<ValueFundResource>>(valueFunds);
            return valueFundsResource;
        }

        /// <summary>
        /// Find a valuefound by Id
        /// </summary>
        /// <returns>Value Fund</returns>
        /// <response code="200">Returns value fund</response>
        /// <response code="404">Return not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ValueFundResource>> FindAsync(long id)
        {
            var result = await _valueFundService.FindAsync(id);

            if (!result.Success)
                return NotFound(result.Message);

            var valueFundResult = _mapper.Map<ValueFund, ValueFundResource>(result.ValueFund);
            return Ok(valueFundResult);
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
        public async Task<ActionResult<ValueFundResource>> PostAsync([FromBody] ValueFundResource valueFundResource)
        {
            var valueFund = _mapper.Map<ValueFundResource, ValueFund>(valueFundResource);

            var existingFund = await _fundService.FindAsync(valueFund.FundId);

            if (!existingFund.Success)
                return NotFound(existingFund.Message);

            var result = await _valueFundService.SaveAsync(valueFund);

            if (!result.Success)
                return BadRequest(result.Message);

            var valueFundResult = _mapper.Map<ValueFund, ValueFundResource>(result.ValueFund);

            return Ok(valueFundResult);
        }

        /// <summary>
        /// Uodate a value Fund
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fundResource"></param>
        /// <returns>The value fund updated</returns>
        /// <response code="200">The value fund updated</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Fund not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ValueFundResource>> PutAsync(long id, [FromBody] ValueFundResource valueFundResource)
        {
            var valueFund = _mapper.Map<ValueFundResource, ValueFund>(valueFundResource);

            var existingFund = await _fundService.FindAsync(valueFund.FundId);

            if (!existingFund.Success)
                return NotFound(existingFund.Message);

            var resultFindValueFund = await _valueFundService.FindAsync(id);

            if (!resultFindValueFund.Success)
                return NotFound(resultFindValueFund.Message);

            var result = await _valueFundService.UpdateAsync(valueFund);

            if (!result.Success)
                return BadRequest(result.Message);

            var valueFundResult = _mapper.Map<ValueFund, ValueFundResource>(result.ValueFund);

            return Ok(valueFundResult);
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
        public async Task<ActionResult<ValueFundResource>> DeleteAsync(long id)
        {
            var resultFindValueFund = await _valueFundService.FindAsync(id);

            if (!resultFindValueFund.Success)
                return NotFound(resultFindValueFund.Message);

            var result = await _valueFundService.DeleteAsync(resultFindValueFund.ValueFund);

            if (!result.Success)
                return BadRequest(result.Message);

            var valueFundResource = _mapper.Map<ValueFund, ValueFundResource>(result.ValueFund);
            return Ok(valueFundResource);
        }
    }
}
