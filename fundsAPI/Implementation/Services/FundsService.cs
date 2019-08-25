using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funds_api.Implementation.Models;
using funds_api.Implementation.Repositories;
using funds_api.Implementation.Resources;
using funds_api.Implementation.Services.Communication;

namespace funds_api.Implementation.Services
{
    public class FundsService : IFundsService
    {
        private readonly IFundsRepository _fundsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FundsService(IFundsRepository fundsRepository, IUnitOfWork unitOfWork)
        {
            _fundsRepository = fundsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Fund>> ListAsync()
        {
            return await _fundsRepository.ListAsync();
        }

        public async Task<FundResponse> SaveAsync(Fund fund)
        {
            try
            {
                await _fundsRepository.AddAsync(fund);
                await _unitOfWork.CompleteAsync();

                return new FundResponse(fund);
            }
            catch(Exception ex)
            {
                return new FundResponse($"An error occurred when saving the fund: {ex.Message}");
            }
        }

        public async Task<FundResponse> FindAsync(long id)
        {
            var existingFund = await _fundsRepository.FindByIdAsync(id);

            if (existingFund == null)
                return new FundResponse("Fund not found");

            return new FundResponse(existingFund);
        }

        public async Task<FundResponse> UpdateAsync(Fund fund)
        {
            try
            {
                _fundsRepository.Update(fund);
                await _unitOfWork.CompleteAsync();

                return new FundResponse(fund);
            }
            catch(Exception ex)
            {
                return new FundResponse($"An error occurred when updating the fund: {ex.Message}");
            }
        }

        public async Task<FundResponse> DeleteAsync(Fund fund)
        {
            try
            {
                _fundsRepository.Remove(fund);
                await _unitOfWork.CompleteAsync();

                return new FundResponse(fund);
            }
            catch (Exception ex)
            {
                return new FundResponse($"An error occurred when deleting the fund: {ex.Message}");
            }

        }
    }
}
