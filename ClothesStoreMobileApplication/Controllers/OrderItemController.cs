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
