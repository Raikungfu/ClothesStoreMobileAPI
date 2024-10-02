using ClothesStoreMobileApplication.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            var obj = _unitOfWork.Order.GetFirstOrDefault(u => u.CustomerId == customerId);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }
    }
}
