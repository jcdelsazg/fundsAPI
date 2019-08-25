using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using funds_api.Implementation.Controllers;
using funds_api.Implementation.Models;
using funds_api.Implementation.Resources;
using funds_api.Implementation.Services;
using funds_api.Implementation.Services.Communication;
using Xunit;

namespace XUnitFundsAPI
{
    public class ValueFundControllerTest
    {
        ValueFundController _controller;
        Mock<IValueFundService> serviceMock;
        Mock<IMapper> mapperMock;
        Mock<IFundsService> fundService;

        Fund fund;
        ValueFund valueFund;
        long id;
        ValueFundResponse valueFundResponseNotFound;
        FundResponse fundReponseNotFound;
        ValueFundResource valueFundResource;

        public ValueFundControllerTest()
        {
            serviceMock = new Mock<IValueFundService>();
            mapperMock = new Mock<IMapper>();
            fundService = new Mock<IFundsService>();
            _controller = new ValueFundController(serviceMock.Object, mapperMock.Object, fundService.Object);

            fund = new Fund { Id = 1, Name = "Fund1", Description = "Description fund 1" };
            fundReponseNotFound = new FundResponse("Fund not found");
            valueFundResponseNotFound = new ValueFundResponse("Value fund not found");
            valueFund = new ValueFund { Id = 1, DateFund = DateTime.UtcNow, Value = 100, FundId = 1 };
            valueFundResource = new ValueFundResource { Id = 1, DateFund = DateTime.UtcNow, Value = 100, FundId = 1 };
            id = 1L;
        }

        [Fact]
        public async Task GetAllValueFundsTest()
        {
            var valueFundList = new List<ValueFund> {
                valueFund
            };
            var valueFunResourceList = new List<ValueFundResource> { valueFundResource };

            serviceMock.Setup(x => x.ListAsync()).ReturnsAsync(valueFundList);
            mapperMock.Setup(m => m.Map<IEnumerable<ValueFund>, IEnumerable<ValueFundResource>>(It.IsAny<IEnumerable<ValueFund>>())).Returns(valueFunResourceList);

            var result = await _controller.GetAllAsync();

            serviceMock.Verify(x => x.ListAsync(), Times.Once);
            mapperMock.Verify(x => x.Map<IEnumerable<ValueFund>, IEnumerable<ValueFundResource>>(valueFundList), Times.Once);
            Assert.Equal(result, valueFunResourceList);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetAllValueFundsEmptyResultTest()
        {
            serviceMock.Setup(x => x.ListAsync()).ReturnsAsync(new List<ValueFund>());
            mapperMock.Setup(m => m.Map<IEnumerable<ValueFund>, IEnumerable<ValueFundResource>>(It.IsAny<IEnumerable<ValueFund>>())).Returns(new List<ValueFundResource>());

            var result = await _controller.GetAllAsync();

            Assert.Empty(result);
            serviceMock.Verify(x => x.ListAsync(), Times.Once);
            mapperMock.Verify(x => x.Map<IEnumerable<ValueFund>, IEnumerable<ValueFundResource>>(new List<ValueFund>()), Times.Once);
        }

        [Fact]
        public async Task GetValueFundByIdNotFoundTest()
        {
            var id = 2L;

            serviceMock.Setup(x => x.FindAsync(id)).ReturnsAsync(valueFundResponseNotFound);
            mapperMock.Setup(m => m.Map<ValueFund, ValueFundResource>(It.IsAny<ValueFund>())).Returns(valueFundResource);

            var result = await _controller.FindAsync(id);

            serviceMock.Verify(x => x.FindAsync(id), Times.Once);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetValueFundByIdFoundTest()
        {
            serviceMock.Setup(x => x.FindAsync(id)).ReturnsAsync(new ValueFundResponse(valueFund));
            mapperMock.Setup(m => m.Map<ValueFund, ValueFundResource>(It.IsAny<ValueFund>())).Returns(valueFundResource);

            var result = await _controller.FindAsync(id);

            serviceMock.Verify(x => x.FindAsync(id), Times.Once);
            mapperMock.Verify(x => x.Map<ValueFund, ValueFundResource>(valueFund), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateValueFundTest()
        {
            mapperMock.Setup(m => m.Map<ValueFundResource, ValueFund>(It.IsAny<ValueFundResource>())).Returns(valueFund);
            fundService.Setup(x => x.FindAsync(valueFund.FundId)).ReturnsAsync(new FundResponse(fund));
            serviceMock.Setup(x => x.SaveAsync(valueFund)).ReturnsAsync(new ValueFundResponse(valueFund));
            mapperMock.Setup(m => m.Map<ValueFund, ValueFundResource>(It.IsAny<ValueFund>())).Returns(valueFundResource);

            var result = await _controller.PostAsync(valueFundResource);

            serviceMock.Verify(x => x.SaveAsync(valueFund), Times.Once);
            mapperMock.Verify(x => x.Map<ValueFund, ValueFundResource>(valueFund), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateValueFundFailTest()
        {
            mapperMock.Setup(m => m.Map<ValueFundResource, ValueFund>(It.IsAny<ValueFundResource>())).Returns(valueFund);
            fundService.Setup(x => x.FindAsync(valueFund.FundId)).ReturnsAsync(new FundResponse(fund));
            serviceMock.Setup(x => x.SaveAsync(valueFund)).ReturnsAsync(valueFundResponseNotFound);

            var result = await _controller.PostAsync(valueFundResource);

            serviceMock.Verify(x => x.SaveAsync(valueFund), Times.Once);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateValueFundTest()
        {
            var valueFund2 = new ValueFund { Id = 2, DateFund = DateTime.UtcNow, Value = 200, FundId = 1 };
            var valueFundResource = new ValueFundResource { Id = 2, DateFund = DateTime.UtcNow, Value = 200, FundId = 1 };

            mapperMock.Setup(m => m.Map<ValueFundResource, ValueFund>(It.IsAny<ValueFundResource>())).Returns(valueFund2);
            fundService.Setup(x => x.FindAsync(valueFund2.FundId)).ReturnsAsync(new FundResponse(fund));
            serviceMock.Setup(x => x.FindAsync(id)).ReturnsAsync(new ValueFundResponse(valueFund));
            serviceMock.Setup(x => x.UpdateAsync(valueFund2)).ReturnsAsync(new ValueFundResponse(valueFund2));
            mapperMock.Setup(m => m.Map<ValueFund, ValueFundResource>(It.IsAny<ValueFund>())).Returns(valueFundResource);

            var result = await _controller.PutAsync(id, valueFundResource);

            serviceMock.Verify(x => x.UpdateAsync(valueFund2), Times.Once);
            mapperMock.Verify(x => x.Map<ValueFund, ValueFundResource>(valueFund2), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateFundFailTest()
        {
            mapperMock.Setup(m => m.Map<ValueFundResource, ValueFund>(It.IsAny<ValueFundResource>())).Returns(valueFund);
            fundService.Setup(x => x.FindAsync(valueFund.FundId)).ReturnsAsync(new FundResponse(fund));
            serviceMock.Setup(x => x.FindAsync(id)).ReturnsAsync(new ValueFundResponse(valueFund));
            serviceMock.Setup(x => x.UpdateAsync(valueFund)).ReturnsAsync(valueFundResponseNotFound);

            var result = await _controller.PutAsync(id, valueFundResource);

            serviceMock.Verify(x => x.UpdateAsync(valueFund), Times.Once);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteValueFundTest()
        {
            serviceMock.Setup(x => x.FindAsync(id)).ReturnsAsync(new ValueFundResponse(valueFund));
            serviceMock.Setup(x => x.DeleteAsync(valueFund)).ReturnsAsync(new ValueFundResponse(valueFund));

            var result = await _controller.DeleteAsync(id);

            serviceMock.Verify(x => x.DeleteAsync(valueFund), Times.Once);
            mapperMock.Verify(x => x.Map<ValueFund, ValueFundResource>(valueFund), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteFundFailTest()
        {
            serviceMock.Setup(x => x.FindAsync(id)).ReturnsAsync(new ValueFundResponse(valueFund));
            serviceMock.Setup(x => x.DeleteAsync(valueFund)).ReturnsAsync(valueFundResponseNotFound);

            var result = await _controller.DeleteAsync(id);

            serviceMock.Verify(x => x.DeleteAsync(valueFund), Times.Once);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
