using AutoMapper;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.ViewModels.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var objList = _unitOfWork.Order.GetAll();
            return Ok(objList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var obj = _unitOfWork.Order.GetFirstOrDefault(u => u.OrderId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("GetOrder/{customerId:int}", Name = "GetOrderByCustomer")]
        public IActionResult GetOrder(int customerId)
        {
            var obj = _unitOfWork.Order.GetAll(u => u.CustomerId == customerId);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        // [HttpGet("GetOrderItem/{userId:int}", Name = "GetOrderByUserId")]
        // public IActionResult GetOrderItem(int userId)
        // {
        //     var obj = _unitOfWork.Order.GetAll(u => u.Customer.User.UserId == userId);
        //     if (obj == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(obj);
        // }


   
   [HttpGet("GetOrderItem", Name = "GetOrderByUserId")]

   public IActionResult GetOrderItem()
   {
       var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
       if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int userId))
       {
           return Unauthorized("User not logged in. Please log in to continue.");
       }


       var order = _unitOfWork.Order.GetAll(u => u.Customer.User.UserId == userId);
    
       if (order == null || !order.Any())
       {
           return NotFound("No order found for this user.");
       }

       return Ok(order);
    }

        


//         [HttpPost]
//         public IActionResult Post([FromBody] OrderCreateViewModel OrderVM)
//         {
//             try
//             {
//                 if (!ModelState.IsValid)
//                 {
//                     return BadRequest(ModelState);
//                 }

//                 //Check Cart của Customer có tồn tại không
//                 //Nếu không thì trả về BadRequest
//                 //Nếu có thì mới tiến hành tạo Order

//                 var cart = _unitOfWork.Cart.GetFirstOrDefault(u => u.CustomerId == OrderVM.CustomerId);
//                 if (cart == null)
//                 {
//                     return BadRequest("Cart của Customer không tồn tại");
//                 }
//                 else
//                 {
//                     //Tạo mới Order
//                     Order order = _mapper.Map<Order>(OrderVM);
//                     Customer customer = _unitOfWork.Customer.GetFirstOrDefault(u => u.CustomerId == OrderVM.CustomerId);    
//                     if (string.IsNullOrEmpty(OrderVM.ShipName))
//                     {
//                         if(customer.Name != null)
//                         {
//                             order.ShipName = customer.Name;
//                         }
//                         else
//                         {
//                             return BadRequest("Please update your Customer Information or Enter your Ship Name!");
//                         }
//                     }
//                     if (string.IsNullOrEmpty(OrderVM.ShipAddress))
//                     {
//                         if (customer.Address != null)
//                         {
//                             order.ShipAddress = customer.Address;
//                         }
//                         else
//                         {
//                             return BadRequest("Please update your Customer Information or Enter your Ship Address!");
//                         }
//                     }
//                     if (string.IsNullOrEmpty(OrderVM.ShipPhone))
//                     {
//                         order.ShipPhone = _unitOfWork.Customer.GetFirstOrDefault(u => u.CustomerId == OrderVM.CustomerId, includeProperties: "User").User.Phone;
//                     }
//                     if (string.IsNullOrEmpty(OrderVM.ShipMail))
//                     {
//                         order.ShipMail = _unitOfWork.Customer.GetFirstOrDefault(u => u.CustomerId == OrderVM.CustomerId, includeProperties: "User").User.Email;
//                     }
//                     _unitOfWork.Order.Add(order);
//                     _unitOfWork.Save();

//                     //Add Order item từ CartItem
//                     var cartItems = _unitOfWork.CartItem.GetAll(u => u.CartId == cart.CartId, includeProperty: "Product");
//                     foreach (var item in cartItems)
//                     {
//                         //check số lượng sản phẩm trong kho
//                         if (item.Product.Quantity < item.Quantity)
//                         {
//                             return BadRequest("Số lượng sản phẩm trong kho không đủ");
//                         }
//                         else
//                         {
//                             OrderItem orderItem = new OrderItem
//                             {
//                                 OrderId = order.OrderId,
//                                 ProductId = item.ProductId,
//                                 Quantity = item.Quantity
//                             };
//                             _unitOfWork.OrderItem.Add(orderItem);
//                             _unitOfWork.Save();
//                             //Trừ số lượng sản phẩm trong kho
//                             item.Product.Quantity -= item.Quantity;
//                             item.Product.QuantitySold += (uint)item.Quantity;
//                             _unitOfWork.Product.Update(item.Product);
//                             _unitOfWork.Save();
//                         }
//                         //Xoas CartItem
//                         _unitOfWork.CartItem.Remove(item);
//                         _unitOfWork.Save();
//                     }

//                     //Tính tổng tiền
//                     order.TotalAmount = 0;
//                     foreach (var item in _unitOfWork.OrderItem.GetAll(u => u.OrderId == order.OrderId, includeProperty: "Product"))
//                     {
//                         order.TotalAmount += item.Product.NewPrice * item.Quantity;
//                     }

//                     //Check DiscountCode
//                     if (!string.IsNullOrEmpty(OrderVM.DiscountCode))
//                     {
//                         var discount = _unitOfWork.Discount.GetFirstOrDefault(u => u.Code == OrderVM.DiscountCode);
//                         if (discount == null)
//                         {
//                             order.DiscountCode = "";
//                         }
//                         else
//                         {
//                             order.DiscountCode = OrderVM.DiscountCode;
//                             //update Discount
//                             discount.Quantity--;
//                             _unitOfWork.Discount.Update(discount);
//                             _unitOfWork.Save();
//                             //Tính lại TotalAmount
//                             order.TotalAmount = order.TotalAmount /100 *(100-(double)discount.DiscountPercentage);
//                         }
//                     }
//                     _unitOfWork.Order.Update(order);
//                     _unitOfWork.Save();

//                     //Xoas Cart
//                     _unitOfWork.Cart.Remove(cart);
//                     _unitOfWork.Save();

//                     return Ok(order);
//                 }
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//             }
//         }
//     }
// }


        [HttpPut("update-status/{id:int}")]
        public IActionResult UpdateOrderStatus(int id, [FromBody] string newStatus)
        {
            var order = _unitOfWork.Order.GetFirstOrDefault(u => u.OrderId == id);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            order.Status = newStatus;
            _unitOfWork.Order.Update(order);
            _unitOfWork.Save();

            return Ok(order);
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteOrder(int id)
        {    var order = _unitOfWork.Order.GetFirstOrDefault(u => u.OrderId == id);
             if (order == null)
             {
                 return NotFound("Order not found.");
             }

             var orderItems = _unitOfWork.OrderItem.GetAll(u => u.OrderId == id);
             foreach (var item in orderItems)
             {
                 _unitOfWork.OrderItem.Remove(item);
             }
             _unitOfWork.Save();

             _unitOfWork.Order.Remove(order);
             _unitOfWork.Save();

              return Ok("Order deleted successfully.");
        }

          [HttpPost]
        public IActionResult Post([FromBody] OrderCreateViewModel OrderVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int userId))
                {
                    return Unauthorized("User not logged in. Please log in to continue.");
                }

                var customer = _unitOfWork.Customer.GetFirstOrDefault(c => c.UserId == userId);
                var userFollowCustomerId = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == userId);

                if (customer == null)
                {
                    return NotFound("Customer not found.");
                }

                var cart = _unitOfWork.Cart.GetFirstOrDefault(u => u.CustomerId == customer.CustomerId);
                if (cart == null)
                {
                    return BadRequest("Cart của Customer không tồn tại");
                }

                Order order = new Order
                {
                    CustomerId = customer.CustomerId,
                    ShipName = customer.Name,
                    ShipAddress = customer.Address,
                    ShipPhone = userFollowCustomerId.Phone,
                    ShipMail = userFollowCustomerId.Email,
                    OrderDate = DateTime.Now,
                    PaymentMethod = OrderVM.PaymentMethod
                };

                _unitOfWork.Order.Add(order);
                _unitOfWork.Save();

                // Xử lý các CartItem thành OrderItem
                var cartItems = _unitOfWork.CartItem.GetAll(u => u.CartId == cart.CartId, includeProperty: "Product");
                foreach (var item in cartItems)
                {
                    if (item.Product.Quantity < item.Quantity)
                    {
                        return BadRequest("Số lượng sản phẩm trong kho không đủ");
                    }

                    OrderItem orderItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    };
                    _unitOfWork.OrderItem.Add(orderItem);
                    _unitOfWork.Save();

                    // Trừ số lượng sản phẩm trong kho
                    item.Product.Quantity -= item.Quantity;
                    item.Product.QuantitySold += (uint)item.Quantity;
                    _unitOfWork.Product.Update(item.Product);
                    _unitOfWork.Save();
                }

                // Tính tổng tiền
                order.TotalAmount = 0;
                foreach (var item in _unitOfWork.OrderItem.GetAll(u => u.OrderId == order.OrderId, includeProperty: "Product"))
                {
                    order.TotalAmount += item.Product.NewPrice * item.Quantity;
                }

                // Xử lý mã giảm giá (nếu có)
                if (!string.IsNullOrEmpty(OrderVM.DiscountCode))
                {
                    var discount = _unitOfWork.Discount.GetFirstOrDefault(u => u.Code == OrderVM.DiscountCode);
                    if (discount != null)
                    {
                        order.DiscountCode = OrderVM.DiscountCode;
                        discount.Quantity--;
                        _unitOfWork.Discount.Update(discount);
                        _unitOfWork.Save();

                        // Tính lại TotalAmount với Discount
                        order.TotalAmount = order.TotalAmount / 100 * (100 - (double)discount.DiscountPercentage);
                    }
                }

                // Cập nhật Order với tổng số tiền cuối cùng
                _unitOfWork.Order.Update(order);
                _unitOfWork.Save();


                // Xóa CartItem
                cartItems = _unitOfWork.CartItem.GetAll(u => u.CartId == cart.CartId);  
                foreach (var item in cartItems)
                {
                    _unitOfWork.CartItem.Remove(item);
                    _unitOfWork.Save();
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
