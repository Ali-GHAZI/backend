using Api.Controllers;
using Application.DTOs;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _serviceMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _serviceMock = new Mock<IProductService>();
            _controller = new ProductController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithProducts()
        {
            var products = new List<ProductDto> { new ProductDto { Id = 1, Name = "P1", Sku = "SKU1" } };
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(products);

            var result = await _controller.GetAll();

            var ok = result as OkObjectResult;
            ok.Should().NotBeNull();
            ok!.Value.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async Task Get_ReturnsOkWithProduct()
        {
            var product = new ProductDto { Id = 2, Name = "P2", Sku = "SKU2" };
            _serviceMock.Setup(s => s.GetByIdAsync(2)).ReturnsAsync(product);

            var result = await _controller.Get(2);

            var ok = result as OkObjectResult;
            ok.Should().NotBeNull();
            ok!.Value.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task Create_ReturnsOkWithCreatedProduct()
        {
            var dto = new ProductDto { Name = "New", Sku = "SKU-N" };
            var created = new ProductDto { Id = 5, Name = dto.Name, Sku = dto.Sku };
            _serviceMock.Setup(s => s.CreateAsync(dto)).ReturnsAsync(created);

            var result = await _controller.Create(dto);

            var ok = result as OkObjectResult;
            ok.Should().NotBeNull();
            ok!.Value.Should().BeEquivalentTo(created);
        }

        [Fact]
        public async Task Update_CallsServiceAndReturnsOk()
        {
            var dto = new ProductDto { Name = "Up", Sku = "SKU-U" };
            _serviceMock.Setup(s => s.UpdateAsync(It.IsAny<ProductDto>())).Returns(Task.CompletedTask).Verifiable();

            var result = await _controller.Update(3, dto);

            _serviceMock.Verify(s => s.UpdateAsync(It.Is<ProductDto>(d => d.Id == 3 && d.Name == dto.Name)), Times.Once);
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Delete_CallsServiceAndReturnsOk()
        {
            _serviceMock.Setup(s => s.DeleteAsync(4)).Returns(Task.CompletedTask).Verifiable();

            var result = await _controller.Delete(4);

            _serviceMock.Verify(s => s.DeleteAsync(4), Times.Once);
            result.Should().BeOfType<OkResult>();
        }
    }
}
