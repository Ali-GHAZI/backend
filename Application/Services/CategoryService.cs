using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(items);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) throw new NotFoundException("Category not found");
            return _mapper.Map<CategoryDto>(item);
        }

        public async Task<CategoryDto> CreateAsync(CategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            var saved = await _repo.AddAsync(entity);
            return _mapper.Map<CategoryDto>(saved);
        }

        public async Task UpdateAsync(CategoryDto dto)
        {
            var existing = await _repo.GetByIdAsync(dto.Id);
            if (existing == null) throw new NotFoundException("Category not found");

            _mapper.Map(dto, existing);

            await _repo.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
