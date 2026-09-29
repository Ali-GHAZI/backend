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
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _serviceMock;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _serviceMock = new Mock<ICategoryService>();
            _controller = new CategoryController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithCategories()
        {
            var cats = new List<CategoryDto> { new CategoryDto { Id = 1, Name = "C1" } };
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(cats);

            var result = await _controller.GetAll();

            var ok = result as OkObjectResult;
            ok.Should().NotBeNull();
            ok!.Value.Should().BeEquivalentTo(cats);
        }

        [Fact]
        public async Task Get_ReturnsOkWithCategory()
        {
            var cat = new CategoryDto { Id = 2, Name = "C2" };
            _serviceMock.Setup(s => s.GetByIdAsync(2)).ReturnsAsync(cat);

            var result = await _controller.Get(2);

            var ok = result as OkObjectResult;
            ok.Should().NotBeNull();
            ok!.Value.Should().BeEquivalentTo(cat);
        }

        [Fact]
        public async Task Create_ReturnsOkWithCreatedCategory()
        {
            var dto = new CategoryDto { Name = "New" };
            var created = new CategoryDto { Id = 5, Name = dto.Name };
            _serviceMock.Setup(s => s.CreateAsync(dto)).ReturnsAsync(created);

            var result = await _controller.Create(dto);

            var ok = result as OkObjectResult;
            ok.Should().NotBeNull();
            ok!.Value.Should().BeEquivalentTo(created);
        }

        [Fact]
        public async Task Update_CallsServiceAndReturnsOk()
        {
            var dto = new CategoryDto { Name = "Up" };
            _serviceMock.Setup(s => s.UpdateAsync(It.IsAny<CategoryDto>())).Returns(Task.CompletedTask).Verifiable();

            var result = await _controller.Update(3, dto);

            _serviceMock.Verify(s => s.UpdateAsync(It.Is<CategoryDto>(d => d.Id == 3 && d.Name == dto.Name)), Times.Once);
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
