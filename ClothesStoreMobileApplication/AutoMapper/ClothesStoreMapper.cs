using AutoMapper;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.ViewModels.Product;

namespace ClothesStoreMobileApplication.AutoMapper
{
    public class ClothesStoreMapper : Profile
    {
        public ClothesStoreMapper()
        {
            CreateMap<Product,ProductCreateViewModel>().ReverseMap();
            CreateMap<Product, ProductUpdateViewModel>().ReverseMap();
        }
    }
}
