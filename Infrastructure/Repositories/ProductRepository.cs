using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Product> AddAsync(Product entity)
        {
            _db.Products.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Products.FindAsync(id);
            if (entity != null)
            {
                _db.Products.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Products
                            .Include(p => p.Images)
                            .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _db.Products
                            .Include(p => p.Images)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsBySubCategoryAsync(int categoryId)
        {
            return await _db.Products
                .Where(p => p.SubCategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return new List<Product>();

            var key = name.Trim().ToLower();

            return await _db.Products
                            .Include(p => p.Images)
                            .Where(p => p.Name != null && p.Name.ToLower().Contains(key))
                            .ToListAsync();
        }

        public async Task<List<string>> GetProductImageUrlsAsync(int productId)
        {
            return await _db.ProductImages
                .Where(img => img.ProductId == productId)
                .Select(img => img.Url)
                .ToListAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _db.Products.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
