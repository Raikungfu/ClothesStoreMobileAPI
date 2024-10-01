using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.ViewModels.Chat;

namespace ClothesStoreMobileApplication.Repository.IRepository
{
    public interface IChatRepository : IRepository<Chat>
    {
        List<ListChatViewModel> GetChat(int id);
    }
}
