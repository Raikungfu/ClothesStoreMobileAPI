using AutoMapper;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.ViewModels.CartItem;
using ClothesStoreMobileApplication.ViewModels.Category;
using ClothesStoreMobileApplication.ViewModels.ChatMessage;
using ClothesStoreMobileApplication.ViewModels.Option;
using ClothesStoreMobileApplication.ViewModels.Order;
using ClothesStoreMobileApplication.ViewModels.Product;
using ClothesStoreMobileApplication.ViewModels.ReplyReview;
using ClothesStoreMobileApplication.ViewModels.Review;

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

            CreateMap<CartItem, CartItemCreateViewModel>().ReverseMap();

            CreateMap<Review, ReviewViewModel>().ReverseMap();

            CreateMap<ReplyReview, ReplyReviewViewModel>().ReverseMap();
            CreateMap<Order, OrderCreateViewModel>().ReverseMap();
        }
    }
}
