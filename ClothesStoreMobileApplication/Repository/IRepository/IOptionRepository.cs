using ClothesStoreMobileApplication.Models;

namespace ClothesStoreMobileApplication.Repository.IRepository
{
    public interface IOptionRepository : IRepository<Option>
    {
        void Update(Option option);
    }
}
