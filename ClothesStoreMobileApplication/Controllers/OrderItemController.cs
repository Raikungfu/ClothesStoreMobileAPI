using ClothesStoreMobileApplication.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var objList = _unitOfWork.OrderItem.GetAll();
            return Ok(objList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var obj = _unitOfWork.OrderItem.GetFirstOrDefault(u => u.OrderItemId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }



        [HttpGet("GetOrderItemDetail/{orderId:int}", Name = "GetOrderItemDetail")]
        public IActionResult GetOrderItemDetail(int orderId)
        {
             var orderItems = _unitOfWork.OrderItem.GetAll(u => u.OrderId == orderId);
           if (orderItems == null || !orderItems.Any())
            {
                return NotFound("No order items found for this order.");
            }
           var result = orderItems.Select(orderItem => new
            {
                orderItem.OrderItemId,
                orderItem.Quantity,
                orderItem.Note,
                Product = _unitOfWork.Product.GetFirstOrDefault(p => p.ProductId == orderItem.ProductId) 
            }).Select(item => new
            {
                item.OrderItemId,
                item.Quantity,
                item.Note,
                Product = item.Product != null ? new
                {
                    item.Product.ProductId,
                    item.Product.Name,
                    item.Product.Img,
                    item.Product.NewPrice,
                    item.Product.OldPrice,
                    item.Product.Description,
                    item.Product.QuantitySold
                } : null 
            }).ToList();

            return Ok(result);
        }




        [HttpGet("GetOrderItem/{orderId:int}", Name = "GetOrderItem")]
        public IActionResult GetOrderItem(int orderId)
        {
            var obj = _unitOfWork.OrderItem.GetAll(u => u.OrderId == orderId);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }
    }
}
