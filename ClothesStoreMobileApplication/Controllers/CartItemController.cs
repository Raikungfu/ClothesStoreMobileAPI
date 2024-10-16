using AutoMapper;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.ViewModels.CartItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CartItemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var objList = _unitOfWork.CartItem.GetAll();
            return Ok(objList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var obj = _unitOfWork.CartItem.GetFirstOrDefault(u => u.CartItemId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("GetCartItems/{cartId:int}", Name = "GetCartItems")]
        public IActionResult GetCartItems(int cartId)
        {
            var obj = _unitOfWork.CartItem.GetAll(u => u.CartId == cartId);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CartItemCreateViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == obj.ProductId); 
            if(obj.Quantity > product.Quantity)
            {
                ModelState.AddModelError("","Quantity is greater than available quantity");
                return BadRequest(ModelState);
            }
            var objToCreate = _mapper.Map<CartItem>(obj);
            _unitOfWork.CartItem.Add(objToCreate);
            _unitOfWork.Save();
            return Ok(objToCreate);
        }

        [HttpPut]
        public IActionResult UpdateQuantity(int cartItemId, int quantity)
        {
            var obj = _unitOfWork.CartItem.GetFirstOrDefault(u => u.CartItemId == cartItemId);
            if (obj == null)
            {
                return NotFound();
            }

            var product = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == obj.ProductId);
            if (quantity > product.Quantity)
            {
                ModelState.AddModelError("", "Quantity is greater than available quantity");
                return BadRequest(ModelState);
            }

            obj.Quantity = quantity;
            _unitOfWork.Save();
            return Ok(obj);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.CartItem.GetFirstOrDefault(u => u.CartItemId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.CartItem.Remove(obj);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
