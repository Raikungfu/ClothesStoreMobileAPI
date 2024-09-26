using AutoMapper;
using ClothesStoreMobileApplication.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OptionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var objList = _unitOfWork.Option.GetAll();
            return Ok(objList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var obj = _unitOfWork.Option.GetFirstOrDefault(u => u.OptionId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var obj = _unitOfWork.Option.GetFirstOrDefault(u => u.Name == name);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

    }
}
