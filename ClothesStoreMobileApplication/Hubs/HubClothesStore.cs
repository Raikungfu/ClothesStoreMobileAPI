using AutoMapper;
using Azure;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.Service;
using ClothesStoreMobileApplication.Service.IService;
using ClothesStoreMobileApplication.ViewModels.Chat;
using ClothesStoreMobileApplication.ViewModels.ChatMessage;
using ClothesStoreMobileApplication.ViewModels.HubsViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClothesStoreMobileApplication.Hubs
{
    [AllowAnonymous]
    public class HubClothesStore<T> : Hub
    {
        private readonly IConnectionMappingService _connectionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HubClothesStore(IUnitOfWork unitOfWork, IMapper mapper, IConnectionMappingService connectionService)
        {
            _connectionService = connectionService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task SendMessage(int receiver, T message)
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

        public async Task<MessageResponseViewModel> SendMessageToUser(T message)
        {
            ChatMessageViewModel chatMessageViewModel = message as ChatMessageViewModel;
            var userIdClaim = Context.User?.FindFirst("Id");

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int senderId))
            {
                var response = new MessageResponseViewModel
                {
                    Status = "Error",
                    Content = "User not found or invalid ID"
                };
                await Clients.Caller.SendAsync("ErrorMessage", response);
                return response;
            }

            chatMessageViewModel.SenderId = senderId;

            var room = _unitOfWork.Chat.GetFirstOrDefault(x => x.RoomId == chatMessageViewModel.RoomId);
            if (room == null)
            {
                var response = new MessageResponseViewModel
                {
                    Status = "Error",
                    Content = "Room not found"
                };
                await Clients.Caller.SendAsync("ErrorMessage", response);
                return response;
            }

            if (senderId != room.UserId1 && senderId != room.UserId2)
            {
                var response = new MessageResponseViewModel
                {
                    Status = "Error",
                    Content = "Sender not in the room"
                };
                await Clients.Caller.SendAsync("ErrorMessage", response);
                return response;
            }

            var chatMessage = _mapper.Map<ChatMessage>(chatMessageViewModel);
            _unitOfWork.ChatMessage.Add(chatMessage);
            _unitOfWork.Save();

            var receiverId = room.UserId1 == senderId ? room.UserId2.ToString() : room.UserId1.ToString();
            chatMessageViewModel.IsSender = room.UserId1 == senderId;
            var messageResponse = new MessageResponseViewModel
            {
                Status = "Success",
                Response = chatMessageViewModel
            };

            await Clients.Users(_connectionService.GetConnectionIds(receiverId)).SendAsync("ReceiveMessage", messageResponse);
            return messageResponse;
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
                _connectionService.AddUserConnection(userId, Context.ConnectionId);

                await Groups.AddToGroupAsync("ClothesStore", userIdClaim.Value);
                await base.OnConnectedAsync();
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _connectionService.RemoveUserConnection(Context.ConnectionId);

            var userIdClaim = Context.User?.FindFirst("Id");

            if(userIdClaim != null) await Groups.RemoveFromGroupAsync("ClothesStore", userIdClaim.Value);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
