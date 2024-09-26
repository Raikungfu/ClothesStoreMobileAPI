using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.IRepository;
using System;

namespace ClothesStoreMobileApplication.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClothesStoreContext _db;
        public UnitOfWork(ClothesStoreContext db)
        {
            _db = db;
            Product = new ProductRepository(_db);

        }

        public IProductRepository Product { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
