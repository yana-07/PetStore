namespace PetStore.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetStore.Common;
    using PetStore.Data.Models;
    using PetStore.Services.Data;
    using PetStore.Services.Mapping;
    using PetStore.Web.ViewModels.Products;

    public class ProductsController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;

        public ProductsController(
            IProductsService productsService,
            ICategoriesService categoriesService)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
        }

        public IActionResult All(string search)
        {
            var allProducts = this.productsService.GetAllByName(search);
            var categories = this.productsService.GetAllProductsCategories();

            var viewModel = new AllProductsViewModel
            {
                AllProducts = allProducts.To<ListAllProductsViewModel>().ToArray(),
                Categories = categories,
                SearchQuery = search,
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var viewModel = new CreateProductInputModel
            {
                Categories = this.categoriesService.All()
                      .To<ListCategoriesOnProductCreateViewModel>().ToArray(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateProductInputModel input)
        {
            if (!this.ModelState.IsValid || !this.categoriesService.ExistsById(input.CategoryId))
            {
                input.Categories = this.categoriesService.All()
                      .To<ListCategoriesOnProductCreateViewModel>().ToArray();

                return this.View(input);
            }

            var product = AutoMapperConfig.MapperInstance.Map<Product>(input);
            await this.productsService.AddProduct(product);

            return this.Redirect("/Products/All");
        }

        public async Task<IActionResult> Details(string id)
        {
            var product = await this.productsService.GetByIdAsync(id);

            if (product == null)
            {
                return this.Redirect("/Home/Error");
            }

            var viewModel = AutoMapperConfig.MapperInstance.Map<ProductDetailsViewModel>(product);

            return this.View(viewModel);
        }
    }
}
