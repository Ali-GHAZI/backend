using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(items);
        }

        public Task<List<string>> GetProductImageUrlsAsync(int productId)
        {
            return _repo.GetProductImageUrlsAsync(productId);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
             if (item == null) throw new NotFoundException("Product not found");
            return _mapper.Map<ProductDto>(item);
        }

        public async Task<ProductDto> CreateAsync(ProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            var saved = await _repo.AddAsync(entity);
            return _mapper.Map<ProductDto>(saved);
        }

        public async Task UpdateAsync(ProductDto dto)
        {
            var existing = await _repo.GetByIdAsync(dto.Id);
            if (existing == null) throw new NotFoundException("Product not found");

            _mapper.Map(dto, existing);

            await _repo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
