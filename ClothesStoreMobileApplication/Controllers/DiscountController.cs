using ClothesStoreMobileApplication.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiscountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var objList = _unitOfWork.Discount.GetAll();
            return Ok(objList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var obj = _unitOfWork.Discount.GetFirstOrDefault(u => u.DiscountId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("bycode/{code}")]
        public IActionResult GetByCode(string code)
        {
            var obj = _unitOfWork.Discount.GetFirstOrDefault(u => u.Code == code);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }


        [HttpGet("bystatus/{status}")]
        public IActionResult GetByStatus(bool status)
        {
            var obj = _unitOfWork.Discount.GetFirstOrDefault(u => u.Status == status);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }
    }
}
