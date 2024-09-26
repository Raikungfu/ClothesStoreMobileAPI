using AutoMapper;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.ViewModels.Chat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChatController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var objList = _unitOfWork.Chat.GetAll();
            return Ok(objList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var obj = _unitOfWork.Chat.GetFirstOrDefault(u => u.RoomId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult Post(ChatViewModel chatvm)
        {
            try
            {
                var obj = _unitOfWork.Chat.GetFirstOrDefault(u => u.CustomerId == chatvm.CustomerId && u.SellerId == chatvm.SellerId);
                if (obj == null)
                {
                    var chat = new Chat
                    {
                        CustomerId = chatvm.CustomerId,
                        SellerId = chatvm.SellerId
                    };
                    _unitOfWork.Chat.Add(chat);
                    _unitOfWork.Save();
                    return Ok(chat);
                }
                else
                {
                    return BadRequest("This Room is created before or seller!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Customer or Seller does not exist");
            }
            
        }
    }
}
