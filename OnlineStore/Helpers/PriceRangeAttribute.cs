using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OnlineStore.ViewModels.Products;

namespace OnlineStore.Helpers
{
    public class PriceRangeAttribute: ValidationAttribute
    {
        private readonly decimal _min;
        private readonly decimal _max;

        public PriceRangeAttribute(double min, double max)
        {
            _min = (decimal) min;
            _max = (decimal) max;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ProductViewModel productViewModel = (ProductViewModel)validationContext.ObjectInstance;
            if (productViewModel.Price <= _min || productViewModel.Price > _max)
            {
                return new ValidationResult($"Cena powinna zawierać się w przedziale {_min}-{_max} PLN");
            }
            return ValidationResult.Success;
        }
    }
}