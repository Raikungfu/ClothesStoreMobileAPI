using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.DataAccess.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;
using System.ComponentModel.DataAnnotations;

namespace ClothesStoreMobileApplication.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ClothesStoreContext _db;

        public OrderRepository(ClothesStoreContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Order obj)
        {
            var objFromDb = _db.Orders.FirstOrDefault(s => s.OrderId == obj.OrderId);
            if (objFromDb != null)
            {
                objFromDb.ShipName = obj.ShipName;
                objFromDb.ShipMail = obj.ShipMail;
                objFromDb.ShipPhone = obj.ShipPhone;
                objFromDb.ShipAddress = obj.ShipAddress;
                objFromDb.PaymentMethod = obj.PaymentMethod;
                objFromDb.Status = obj.Status;
            }
        }
    }
}
