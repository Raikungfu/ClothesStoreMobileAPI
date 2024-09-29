using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;

namespace ClothesStoreMobileApplication.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ClothesStoreContext _db;

        public ProductRepository(ClothesStoreContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product product)
        {
            var objFromDb = _db.Products.FirstOrDefault(s => s.ProductId == product.ProductId);
            if (objFromDb != null)
            {
                if (product.Img != null)
                {
                    objFromDb.Img = product.Img;
                }
                objFromDb.Name = product.Name;
                objFromDb.Quantity = product.Quantity;
                objFromDb.Description = product.Description;
                objFromDb.NewPrice = product.NewPrice;
                objFromDb.OldPrice = product.OldPrice;
                objFromDb.QuantitySold = product.QuantitySold;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.SellerId = product.SellerId;
            }
        }
        
        public IEnumerable<Product> GetProducts(bool orderByLatest = false, bool orderByMostDiscount = false, bool orderByMostSales = false, string includeProperties = null)
        {
            IQueryable<Product> query = _db.Products;

            if (orderByLatest)
            {
                query = query.OrderByDescending(p => p.ProductId);
            }
            else if (orderByMostDiscount)
            {
                query = query.OrderByDescending(p => (double) (p.OldPrice - p.NewPrice) / p.OldPrice);
            }
            else if (orderByMostSales)
            {
                query = query.OrderByDescending(p => p.QuantitySold);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.ToList();
        }
    }
}
