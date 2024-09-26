using AutoMapper;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.ViewModels.Category;
using ClothesStoreMobileApplication.ViewModels.ChatMessage;
using ClothesStoreMobileApplication.ViewModels.Option;
using ClothesStoreMobileApplication.ViewModels.Product;

namespace ClothesStoreMobileApplication.AutoMapper
{
    public class ClothesStoreMapper : Profile
    {
        public ClothesStoreMapper()
        {
            CreateMap<Product,ProductCreateViewModel>().ReverseMap();
            CreateMap<Product, ProductUpdateViewModel>().ReverseMap();

            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<Option, OptionViewModel>().ReverseMap();

            CreateMap<ChatMessage,ChatMessageViewModel>().ReverseMap();
        }
    }
}
