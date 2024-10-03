namespace ClothesStoreMobileApplication.Service.IService
{
    public interface IConnectionMappingService
    {
        Task AddUserConnection(string userId, string connectionId);
        Task RemoveUserConnection(string connectionId);
        string GetUserId(string connectionId);
        List<string> GetConnectionIds(string userId);
    }
}
