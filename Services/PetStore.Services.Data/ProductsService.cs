namespace PetStore.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PetStore.Data.Common.Repositories;
    using PetStore.Data.Models;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task AddProduct(Product product)
        {
            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public IQueryable<Product> GetAllByCategory(string categoryName)
        {
            if (categoryName != null)
            {
                return this.productsRepository.AllAsNoTracking()
                    .Where(x => x.Category.Name.ToLower().Contains(categoryName.ToLower()));
            }

            return this.productsRepository.AllAsNoTracking();
        }

        public IQueryable<Product> GetAllByName(string nameSearch)
        {
            if (nameSearch != null)
            {
                return this.productsRepository.AllAsNoTracking()
                    .Where(x => x.Name.ToLower().Contains(nameSearch.ToLower()));
            }

            return this.productsRepository.AllAsNoTracking();
        }

        public ICollection<string> GetAllProductsCategories()
        {
            return this.productsRepository
                .AllAsNoTracking()
                .Select(x => x.Category.Name)
                .Distinct()
                .ToArray();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await this.productsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
