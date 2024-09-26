using ClothesStoreMobileApplication.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOptionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductOptionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var objList = _unitOfWork.ProductOption.GetAll();
            return Ok(objList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var obj = _unitOfWork.ProductOption.GetFirstOrDefault(u => u.ProductOptionsId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var obj = _unitOfWork.ProductOption.GetFirstOrDefault(u => u.Name == name);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }
    }
}
