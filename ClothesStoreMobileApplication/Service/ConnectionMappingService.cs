using ClothesStoreMobileApplication.ViewModels.HubsViewModel;

namespace ClothesStoreMobileApplication.Service
{
    public class ConnectionMappingService
    {
        private readonly Dictionary<string, int> _connections = new Dictionary<string, int>();

        public void AddConnection(int id, string connectionId)
        {
            _connections[connectionId] = id;
        }

        public void RemoveConnection(string connect)
        {
            _connections.Remove(connect);
        }

        public int GetId(string connect)
        {
            return _connections[connect];
        }

        public List<string> GetConnect(int id)
        {
            return _connections.Where(x => x.Value == id).Select(x => x.Key).ToList();
        }
    }
}