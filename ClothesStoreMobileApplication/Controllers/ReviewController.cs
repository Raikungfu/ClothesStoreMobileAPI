using AutoMapper;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.ViewModels.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReviewController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var objList = _unitOfWork.Review.GetAll();
            return Ok(objList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var obj = _unitOfWork.Review.GetFirstOrDefault(u => u.ReviewId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("GetReviewsForOrder/{orderId}")]
        public IActionResult GetReviewsForOrder(int orderId)
        {
            var objList = _unitOfWork.Review.GetAll(u => u.OrderId == orderId);
            if (objList == null)
            {
                return NotFound();
            }
            return Ok(objList);
        }

        [HttpGet("GetReviewsForProduct/{productId}")]
        public IActionResult GetReviewsForProduct(int productId)
        {
            var objList = _unitOfWork.Review.GetAll(u => u.ProductId == productId);
            if (objList == null)
            {
                return NotFound();
            }
            return Ok(objList);
        }

        [HttpGet("GetReviewsForCustomer/{customerId}")]
        public IActionResult GetReviewsForCustomer(int customerId)
        {
            var objList = _unitOfWork.Review.GetAll(u => u.CustomerId == customerId);
            if (objList == null)
            {
                return NotFound();
            }
            return Ok(objList);
        }


        [HttpDelete("{id:int}", Name = "DeleteReview")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Review.GetFirstOrDefault(u => u.ReviewId == id);
            if (objFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Review.Remove(objFromDb);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpPost]
        public IActionResult Create([FromBody] ReviewViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var IsReviewExists = _unitOfWork.Review.GetFirstOrDefault(u => u.CustomerId == obj.CustomerId && u.ProductId == obj.ProductId && u.OrderId == obj.OrderId);
            if (IsReviewExists != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return BadRequest(ModelState);
            }
            else
            {
                var objToCreate = _mapper.Map<Models.Review>(obj);
                _unitOfWork.Review.Add(objToCreate);
                _unitOfWork.Save();
                return Ok(objToCreate);
            }
        }
    }
}
