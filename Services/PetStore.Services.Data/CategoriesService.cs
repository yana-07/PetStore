namespace PetStore.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using PetStore.Data.Common.Repositories;
    using PetStore.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IQueryable<Category> All()
        {
            return this.categoriesRepository.AllAsNoTracking();
        }

        public bool ExistsById(int id)
        {
            return this.categoriesRepository
                .AllAsNoTracking().Any(x => x.Id == id);
        }
    }
}
