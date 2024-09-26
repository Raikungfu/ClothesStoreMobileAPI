using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;
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
    }
}
