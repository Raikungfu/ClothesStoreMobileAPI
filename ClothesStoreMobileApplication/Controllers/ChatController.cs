using AutoMapper;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.Service;
using ClothesStoreMobileApplication.ViewModels.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("listchat")]
        [Authorize]
        public IActionResult GetChat()
        {
            var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int userId))
            {
                throw new Exception("User ID claim is missing or invalid.");
            }

            var chatRooms = _unitOfWork.Chat.GetChat(userId);

            if (chatRooms == null || !chatRooms.Any())
            {
                return NotFound("No chat rooms found for this user.");
            }

            return Ok(chatRooms);
        }

        [HttpPost]
        public IActionResult Post(ChatViewModel chatvm)
        {
            try
            {
                var obj = _unitOfWork.Chat.GetFirstOrDefault(u => u.UserId1 == chatvm.UserId1 && u.UserId2 == chatvm.UserId2);
                if (obj == null)
                {
                    var chat = new Chat
                    {
                        UserId1 = chatvm.UserId1,
                        UserId2 = chatvm.UserId2
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
