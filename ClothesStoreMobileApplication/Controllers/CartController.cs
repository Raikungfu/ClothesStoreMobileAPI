using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var objList = _unitOfWork.Cart.GetAll();
            return Ok(objList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var obj = _unitOfWork.Cart.GetFirstOrDefault(u => u.CartId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("GetCart/{customerId:int}", Name = "GetCart")]
        public IActionResult GetCart(int customerId)
        {
            var obj = _unitOfWork.Cart.GetFirstOrDefault(u => u.CustomerId == customerId);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Cart.GetFirstOrDefault(u => u.CartId == id);
            if (objFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Cart.Remove(objFromDb);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpPost]
        public IActionResult Post(int customerId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isHasCart = _unitOfWork.Cart.GetFirstOrDefault(u => u.CustomerId == customerId);
                if (isHasCart != null)
                {
                    return BadRequest("Cart is already exist");
                }
                var objToCreate = new Cart
                {
                    CustomerId = customerId
                };
                _unitOfWork.Cart.Add(objToCreate);
                _unitOfWork.Save();
                return Ok(objToCreate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
