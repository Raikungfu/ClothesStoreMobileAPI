using AutoMapper;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.Service;
using ClothesStoreMobileApplication.ViewModels.Chat;
using ClothesStoreMobileApplication.ViewModels.ChatMessage;
using ClothesStoreMobileApplication.ViewModels.HubsViewModel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClothesStoreMobileApplication.Hubs
{
    public class HubClothesStore<T> : Hub
    {
        private readonly ConnectionMappingService _connectionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HubClothesStore(ConnectionMappingService connectionService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _connectionService = connectionService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task SendMessage(T message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToCaller(T message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToGroup(string group, T message)
        {
            return Clients.Group(group).SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageUpdate(T post)
        {
            await Clients.All.SendAsync("ReceivePostUpdate", post);
        }

        public async Task SendMessageToUser(ChatMessageViewModel chatMessageVM)
        {
            var userIdClaim = Context.User?.FindFirst("Id");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int senderId))
            {
                var response = new MessageResponseViewModel
                {
                    Status = "Error",
                    Content = "User not found or invalid ID"
                };
                await Clients.Caller.SendAsync("ReceiveMessage", response);
                return;
            }
            chatMessageVM.SenderId = senderId;

            var room = _unitOfWork.Chat.GetFirstOrDefault(x => x.RoomId == chatMessageVM.RoomId);
            if (room == null)
            {
                var response = new MessageResponseViewModel
                {
                    Status = "Error",
                    Content = "Room not found"
                };
                await Clients.Caller.SendAsync("ReceiveMessage", response);
                return;
            }

            if (senderId != room.UserId1 && senderId != room.UserId2)
            {
                var response = new MessageResponseViewModel
                {
                    Status = "Error",
                    Content = "Sender not in the room"
                };
                await Clients.Caller.SendAsync("ReceiveMessage", response);
                return;
            }

            var chatMessage = _mapper.Map<ChatMessage>(chatMessageVM);
            _unitOfWork.ChatMessage.Add(chatMessage);
            _unitOfWork.Save();

            var idReceiver = _unitOfWork.Chat.GetFirstOrDefault(x => x.RoomId == chatMessageVM.RoomId);
            if (idReceiver == null)
            {
                var response = new MessageResponseViewModel
                {
                    Status = "Error",
                    Content = "Receiver not found"
                };
                await Clients.Caller.SendAsync("ReceiveMessage", response);
                return;
            }

            var receiverId = idReceiver.UserId1 == senderId ? idReceiver.UserId2.ToString() : idReceiver.UserId1.ToString();
            var messageResponse = new MessageResponseViewModel
            {
                Status = "Success",
                Content = chatMessageVM.Content,
                SenderId = chatMessageVM.SenderId.ToString(),
                RoomId = chatMessageVM.RoomId,
                Media = chatMessageVM.Media,
                Icon = chatMessageVM.Icon,
                Timestamp = DateTime.UtcNow
            };

            await Clients.User(receiverId).SendAsync("ReceiveMessage", messageResponse);
        }

        public async Task SendMessageToUsers(List<string> ids, T message)
        {
            await Clients.Users(ids).SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToCurrentUser(string message)
        {
            var userId = Context.User?.FindFirst("Id");

            if (userId == null)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", message);
            }
            else
            {
                await Clients.User(userId.Value).SendAsync("ReceiveMessage", message);
            }
        }

        public override async Task OnConnectedAsync()
        {
            var userIdClaim = Context.User?.FindFirst("Id");
            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
                _connectionService.AddConnection(int.Parse(userId), Context.ConnectionId);

                await Groups.AddToGroupAsync(Context.ConnectionId, userIdClaim.Value);
                await base.OnConnectedAsync();
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _connectionService.RemoveConnection(Context.ConnectionId);
            var userIdClaim = Context.User?.FindFirst("Id");
            if(userIdClaim != null) await Groups.RemoveFromGroupAsync(Context.ConnectionId, userIdClaim.Value);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
