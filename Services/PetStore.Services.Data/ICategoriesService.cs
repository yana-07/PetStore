namespace PetStore.Services.Data
{
    using System.Linq;

    using PetStore.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<Category> All();

        bool ExistsById(int id);
    }
}
