using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    public class FundsControllerTest
    {
        FundsController _controller;
        Mock<IFundsService> serviceMock;
        Mock<IMapper> mapperMock;

        Fund fund;
        long id;
        FundResponse fundReponseNotFound;
        FundsResource fundResource;
        SaveFundsResource saveFundResource;

        public FundsControllerTest()
        {
            serviceMock = new Mock<IFundsService>();
            mapperMock = new Mock<IMapper>();
            _controller = new FundsController(serviceMock.Object, mapperMock.Object);

            fundReponseNotFound = new FundResponse("Fund not found");
            fund = new Fund { Id = 1, Name = "Fund1", Description = "Description fund 1" };
            fundResource = new FundsResource { Id = 1, Name = "Fund1", Description = "Description fund 1" };
            saveFundResource = new SaveFundsResource { Name = "Fund1", Description = "Description fund 1" };
            id = 1L;
        }

        [Fact]
        public async Task GetAllFundsTest()
        {
            var fundList = new List<Fund> {
                fund
            };
            var fundResourceList = new List<FundsResource> { fundResource };

            serviceMock.Setup(x => x.ListAsync()).ReturnsAsync(fundList);
            mapperMock.Setup(m => m.Map<IEnumerable<Fund>, IEnumerable<FundsResource>>(It.IsAny<IEnumerable<Fund>>())).Returns(fundResourceList);

            var result = await _controller.GetAllAsync();

            serviceMock.Verify(x => x.ListAsync(), Times.Once);
            mapperMock.Verify(x => x.Map<IEnumerable<Fund>, IEnumerable<FundsResource>>(fundList), Times.Once);
            Assert.Equal(result, fundResourceList);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetAllFundsEmptyResultTest()
        {
            serviceMock.Setup(x => x.ListAsync()).ReturnsAsync(new List<Fund>());
            mapperMock.Setup(m => m.Map<IEnumerable<Fund>, IEnumerable<FundsResource>>(It.IsAny<IEnumerable<Fund>>())).Returns(new List<FundsResource>());

            var result = await _controller.GetAllAsync();

            Assert.Empty(result);
            serviceMock.Verify(x => x.ListAsync(), Times.Once);
            mapperMock.Verify(x => x.Map<IEnumerable<Fund>, IEnumerable<FundsResource>>(new List<Fund>()), Times.Once);
        }

        [Fact]
        public async Task GetFundByIdNotFoundTest()
        {
            var id = 2L;

            serviceMock.Setup(x => x.FindAsync(id)).ReturnsAsync(fundReponseNotFound);
            mapperMock.Setup(m => m.Map<Fund, FundsResource>(It.IsAny<Fund>())).Returns(fundResource);

            var result = await _controller.FindAsync(id);

            serviceMock.Verify(x => x.FindAsync(id), Times.Once);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetFundByIdFoundTest()
        {
            serviceMock.Setup(x => x.FindAsync(id)).ReturnsAsync(new FundResponse(fund));
            mapperMock.Setup(m => m.Map<Fund, FundsResource>(It.IsAny<Fund>())).Returns(fundResource);

            var result = await _controller.FindAsync(id);

            serviceMock.Verify(x => x.FindAsync(id), Times.Once);
            mapperMock.Verify(x => x.Map<Fund, FundsResource>(fund), Times.Once);
            Assert.NotNull(result);           
        }

        [Fact]
        public async Task CreateFundTest()
        {
            mapperMock.Setup(m => m.Map<SaveFundsResource, Fund>(It.IsAny<SaveFundsResource>())).Returns(fund);
            serviceMock.Setup(x => x.SaveAsync(fund)).ReturnsAsync(new FundResponse(fund));
            mapperMock.Setup(m => m.Map<Fund, FundsResource>(It.IsAny<Fund>())).Returns(fundResource);

            var result = await _controller.PostAsync(saveFundResource);

            serviceMock.Verify(x => x.SaveAsync(fund), Times.Once);
            mapperMock.Verify(x => x.Map<Fund, FundsResource>(fund), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public void FundModelInvalidTest()
        {
            var saveFundResource = new SaveFundsResource { Name = "Fund1" };

            var result = Validator.TryValidateObject(saveFundResource, new System.ComponentModel.DataAnnotations.ValidationContext(saveFundResource), new List<ValidationResult>());

            Assert.False(result);
        }

        [Fact]
        public async Task CreateFundFailTest()
        {
            mapperMock.Setup(m => m.Map<SaveFundsResource, Fund>(It.IsAny<SaveFundsResource>())).Returns(fund);
            serviceMock.Setup(x => x.SaveAsync(fund)).ReturnsAsync(fundReponseNotFound);

            var result = await _controller.PostAsync(saveFundResource);

            serviceMock.Verify(x => x.SaveAsync(fund), Times.Once);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateFundTest()
        {
            var fund2 = new Fund { Id = 2, Name = "Fund2", Description = "Description fund 2" };
            var fundResource = new FundsResource { Id = 2, Name = "Fund2", Description = "Description fund 2" };

            mapperMock.Setup(m => m.Map<SaveFundsResource, Fund>(It.IsAny<SaveFundsResource>())).Returns(fund2);
            serviceMock.Setup(x => x.FindAsync(id)).ReturnsAsync(new FundResponse(fund));
            serviceMock.Setup(x => x.UpdateAsync(fund2)).ReturnsAsync(new FundResponse(fund2));
            mapperMock.Setup(m => m.Map<Fund, FundsResource>(It.IsAny<Fund>())).Returns(fundResource);

            var result = await _controller.PutAsync(id, saveFundResource);

            serviceMock.Verify(x => x.UpdateAsync(fund2), Times.Once);
            mapperMock.Verify(x => x.Map<Fund, FundsResource>(fund2), Times.Once);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task UpdateFundFailTest()
        {
            mapperMock.Setup(m => m.Map<SaveFundsResource, Fund>(It.IsAny<SaveFundsResource>())).Returns(fund);
            serviceMock.Setup(x => x.FindAsync(id)).ReturnsAsync(new FundResponse(fund));
            serviceMock.Setup(x => x.UpdateAsync(fund)).ReturnsAsync(fundReponseNotFound);

            var result = await _controller.PutAsync(id, saveFundResource);

            serviceMock.Verify(x => x.UpdateAsync(fund), Times.Once);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteFundTest()
        {
            serviceMock.Setup(x => x.FindAsync(id)).ReturnsAsync(new FundResponse(fund));
            serviceMock.Setup(x => x.DeleteAsync(fund)).ReturnsAsync(new FundResponse(fund));

            var result = await _controller.DeleteAsync(id);

            serviceMock.Verify(x => x.DeleteAsync(fund), Times.Once);
            mapperMock.Verify(x => x.Map<Fund, FundsResource>(fund), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteFundFailTest()
        {
            serviceMock.Setup(x => x.FindAsync(id)).ReturnsAsync(new FundResponse(fund));
            serviceMock.Setup(x => x.DeleteAsync(fund)).ReturnsAsync(fundReponseNotFound);

            var result = await _controller.DeleteAsync(id);

            serviceMock.Verify(x => x.DeleteAsync(fund), Times.Once);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
