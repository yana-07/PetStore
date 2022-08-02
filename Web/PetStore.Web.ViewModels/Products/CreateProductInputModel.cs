namespace PetStore.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using PetStore.Data.Models;
    using PetStore.Data.Models.Common;
    using PetStore.Services.Mapping;

    public class CreateProductInputModel : IMapTo<Product>
    {
        [Required(ErrorMessage = ProductValidationConstants.NameIsRequiredMessage)]
        [MinLength(ProductValidationConstants.NameMinLength, ErrorMessage = ProductValidationConstants.NameMinLengthMessage)]
        [MaxLength(ProductValidationConstants.NameMaxLength, ErrorMessage = ProductValidationConstants.NameMaxLengthMessage)]
        public string Name { get; set; }

        [Range(typeof(decimal), ProductValidationConstants.PriceMinValue, ProductValidationConstants.PriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public ICollection<ListCategoriesOnProductCreateViewModel> Categories { get; set; }
    }
}
