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
                return Unauthorized("User not logged in. Please log in to continue.");
            }

            var chatRooms = _unitOfWork.Chat.GetChat(userId);

            if (chatRooms == null || !chatRooms.Any())
            {
                return NotFound("No chat rooms found for this user.");
            }

            return Ok(chatRooms);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ChatViewModel chatvm)
        {
            var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int userId))
            {
                return Unauthorized("User not logged in. Please log in to continue.");
            }

            if (userId == chatvm.userIdOther)
            {
                return BadRequest("You cannot chat with yourself.");
            }

            try
            {
                var obj = _unitOfWork.Chat.GetFirstOrDefault(u => (u.UserId1 == userId && u.UserId2 == chatvm.userIdOther) || (u.UserId2 == userId && u.UserId1 == chatvm.userIdOther));
                if (obj == null)
                {
                    var chat = new Chat
                    {
                        UserId1 = userId,
                        UserId2 = chatvm.userIdOther
                    };
                    _unitOfWork.Chat.Add(chat);
                    _unitOfWork.Save();
                    return Ok(chat);
                }
                else
                {
                    return Ok(obj);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
