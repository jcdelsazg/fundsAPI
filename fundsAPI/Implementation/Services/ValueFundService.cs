using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funds_api.Implementation.Models;
using funds_api.Implementation.Repositories;
using funds_api.Implementation.Services.Communication;

namespace funds_api.Implementation.Services
{
    public class ValueFundService : IValueFundService
    {
        private readonly IValueFundRepository _valueFundRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ValueFundService(IValueFundRepository valueFundRepository, IUnitOfWork unitOfWork)
        {
            _valueFundRepository = valueFundRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ValueFund>> ListAsync()
        {
            return await _valueFundRepository.ListAsync();
        }

        public async Task<ValueFundResponse> FindAsync(long id)
        {
            var existingValueFund = await _valueFundRepository.FindByIdAsync(id);

            if (existingValueFund == null)
                return new ValueFundResponse("ValueFund not found");

            return new ValueFundResponse(existingValueFund);
        }

        public async Task<ValueFundResponse> SaveAsync(ValueFund valueFund)
        {
            try
            {
                await _valueFundRepository.AddAsync(valueFund);
                await _unitOfWork.CompleteAsync();

                return new ValueFundResponse(valueFund);
            }
            catch (Exception ex)
            {
                return new ValueFundResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<ValueFundResponse> UpdateAsync(ValueFund valueFund)
        {
            try
            {
                _valueFundRepository.Update(valueFund);
                await _unitOfWork.CompleteAsync();

                return new ValueFundResponse(valueFund);
            }
            catch (Exception ex)
            {
                return new ValueFundResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<ValueFundResponse> DeleteAsync(ValueFund valueFUnd)
        {
            try
            {
                _valueFundRepository.Remove(valueFUnd);
                await _unitOfWork.CompleteAsync();

                return new ValueFundResponse(valueFUnd);
            }
            catch (Exception ex)
            {
                return new ValueFundResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }
    }
}
