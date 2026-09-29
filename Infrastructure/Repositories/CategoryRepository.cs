using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Categories
                            .Include(c => c.SubCategories)
                            .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _db.Categories
                            .Include(c => c.SubCategories)
                            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> AddAsync(Category entity)
        {
            _db.Categories.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Category entity)
        {
            _db.Categories.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Categories.FindAsync(id);
            if (entity != null)
            {
                _db.Categories.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}