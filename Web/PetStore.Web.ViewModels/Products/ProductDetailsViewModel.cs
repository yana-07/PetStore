namespace PetStore.Web.ViewModels.Products
{
    using AutoMapper;
    using PetStore.Data.Models;
    using PetStore.Services.Mapping;

    public class ProductDetailsViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductDetailsViewModel>()
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name));
        }
    }
}
