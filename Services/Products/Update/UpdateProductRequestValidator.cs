using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products.Update
{
	public class UpdateProductRequestValidator:AbstractValidator<UpdateProductRequest>
	{
		public UpdateProductRequestValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Product name is required.")
				.MinimumLength(3).WithMessage("Product name must be at least 3 characters.")
				.MaximumLength(50).WithMessage("Product name must not exceed 50 characters.");
			
			RuleFor(x => x.Price)
				.GreaterThan(0).WithMessage("Price must be greater than zero.");

			RuleFor(x => x.CategoryId)
				.GreaterThan(0).WithMessage("Category Id must be greater than zero.");

			RuleFor(x => x.Stock)
				.InclusiveBetween(1, 200).WithMessage("Stock must be between 1 and 250.");
		}
	}
}
