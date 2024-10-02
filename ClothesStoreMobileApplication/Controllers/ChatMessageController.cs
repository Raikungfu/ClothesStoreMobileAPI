using AutoMapper;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.Service;
using ClothesStoreMobileApplication.ViewModels.ChatMessage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChatMessageController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetChatMessages()
        {
            var chatMessages = _unitOfWork.ChatMessage.GetAll();
            return Ok(chatMessages);
        }

        [HttpGet("room/{roomId}")]
        [Authorize]
        public IActionResult GetChatMessagesByRoomId(int roomId)
        {
            int userId = int.Parse(User.FindFirst("Id")?.Value);

            var chatMessages = _unitOfWork.ChatMessage.GetAll(x => x.RoomId == roomId).Select(x => new
            {
                x.MessageId,
                x.RoomId,
                x.SenderId,
                x.Content,
                x.Media,
                x.Icon,
                x.Timestamp,
                IsSender = x.SenderId == userId
            });
            return Ok(chatMessages);
        }

        [HttpPost]
        public IActionResult CreateChatMessage([FromBody] ChatMessageViewModel chatMessageVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var room = _unitOfWork.Chat.GetFirstOrDefault(x => x.RoomId == chatMessageVM.RoomId);
            if (room == null)
            {
                return BadRequest("Room not found");
            }
            if(chatMessageVM.SenderId != room.UserId1 && chatMessageVM.SenderId != room.UserId2)
            {
                return BadRequest("Sender not in the room");
            }

            var chatMessage = _mapper.Map<ChatMessage>(chatMessageVM);
            _unitOfWork.ChatMessage.Add(chatMessage);
            _unitOfWork.Save();
            return StatusCode(201);

        }
    }
}
