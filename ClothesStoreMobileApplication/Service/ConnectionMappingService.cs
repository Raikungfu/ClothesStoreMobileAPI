using ClothesStoreMobileApplication.Service.IService;
using ClothesStoreMobileApplication.ViewModels.HubsViewModel;

namespace ClothesStoreMobileApplication.Service
{
    public class ConnectionMappingService : IConnectionMappingService
    {
        private readonly Dictionary<string, List<string>> _userConnections = new Dictionary<string, List<string>>();

        public Task AddUserConnection(string userId, string connectionId)
        {
            if (!_userConnections.ContainsKey(userId))
            {
                _userConnections[userId] = new List<string>();
            }

            _userConnections[userId].Add(connectionId);
            return Task.CompletedTask;
        }

        public Task RemoveUserConnection(string connectionId)
        {
            foreach (var userId in _userConnections.Keys.ToList())
            {
                _userConnections[userId].Remove(connectionId);
                if (!_userConnections[userId].Any())
                {
                    _userConnections.Remove(userId);
                }
            }
            return Task.CompletedTask;
        }

        public string GetUserId(string connectionId)
        {
            return _userConnections.FirstOrDefault(x => x.Value.Contains(connectionId)).Key;
        }

        public List<string> GetConnectionIds(string userId)
        {
            _userConnections.TryGetValue(userId, out var connectionIds);
            return connectionIds ?? new List<string>();
        }
    }
}