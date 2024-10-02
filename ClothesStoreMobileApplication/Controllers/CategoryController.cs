using AutoMapper;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.ViewModels.Category;
using ClothesStoreMobileApplication.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var objList = _unitOfWork.Category.GetAll();
            return Ok(objList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Name == name);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoryViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var objToCreate = _mapper.Map<Category>(obj);
            _unitOfWork.Category.Add(objToCreate);
            _unitOfWork.Save();
            return Ok(obj);

        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] CategoryViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var objFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            if (objFromDb == null)
            {
                return NotFound();
            }
            _mapper.Map(obj, objFromDb);
            _unitOfWork.Category.Update(objFromDb);
            _unitOfWork.Save();
            return Ok(obj);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            if (objFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
