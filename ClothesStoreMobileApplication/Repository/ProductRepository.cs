using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ClothesStoreMobileApplication.ViewModels.Product;
using AutoMapper;
using ClothesStoreMobileApplication.ViewModels.Option;
using ClothesStoreMobileApplication.ViewModels.User;

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
                query = query.OrderByDescending(p => (double)(p.OldPrice - p.NewPrice) / p.OldPrice);
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

        public ProductDetailViewModel GetProductDetail(int id)
        {
            return _db.Products.AsNoTracking()
            .Where(x => x.ProductId == id)
        .Select(x => new ProductDetailViewModel
        {
            ProductId = x.ProductId,
            Name = x.Name,
            Img = x.Img,
            Quantity = x.Quantity,
            Description = x.Description,
            NewPrice = x.NewPrice,
            OldPrice = x.OldPrice,
            QuantitySold = x.QuantitySold,
            SellerId = x.SellerId,
            RatingPoint = x.Reviews.Any() ? x.Reviews.Average(r => r.Rating) : 0,
            RatingCount = x.Reviews.Count(),
            Options = x.Options.Select(o => new OptionModel
            {
                OptionId = o.OptionId,
                Name = o.ProductOptions.NameDescription,
                ProductOption = o.ProductOptions.Name,
                Price = o.Price
            }).ToList(),
            Seller = new SellerModel
            {
                Avt = x.Seller.Avt,
                CompanyName = x.Seller.CompanyName,
                Address = x.Seller.Address,
                UserId = x.Seller.UserId
            }
        })
        .FirstOrDefault();
        }
    }
}