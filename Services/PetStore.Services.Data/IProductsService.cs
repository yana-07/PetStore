namespace PetStore.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetStore.Data.Models;

    public interface IProductsService
    {
        IQueryable<Product> GetAllByName(string nameSearch = "");

        IQueryable<Product> GetAllByCategory(string categoryName = "");

        ICollection<string> GetAllProductsCategories();

        Task<Product> GetByIdAsync(string id);

        Task AddProduct(Product product);
    }
}
