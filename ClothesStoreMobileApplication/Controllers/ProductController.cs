using AutoMapper;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var objList = _unitOfWork.Product.GetAll(null, null, "Category");
            return Ok(objList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u =>    u.ProductId == id, "Category");
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Name == name, "Category");
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("GetProductInCategory/{categoryId:int}")]
        public IActionResult GetProductInCategory(int categoryId)
        {
            var objList = _unitOfWork.Product.GetAll(u => u.CategoryId == categoryId, null, "Category");
            if (objList == null)
            {
                return NotFound();
            }
            return Ok(objList);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductCreateViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var objToCreate = _mapper.Map<Product>(obj);
            _unitOfWork.Product.Add(objToCreate);
            _unitOfWork.Save();
            return Ok(obj);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] ProductUpdateViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var objFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == id);
            if (objFromDb == null)
            {
                return NotFound();
            }
            _mapper.Map(obj, objFromDb);
            _unitOfWork.Product.Update(objFromDb);
            _unitOfWork.Save();
            return Ok(obj);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == id);
            if (objFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(objFromDb);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
