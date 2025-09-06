using App.Application.Contracts.Persistance;
using FluentValidation;

namespace App.Application.Feature.Products.Create;

public class CreateProductRequestValidator:AbstractValidator<CreateProductRequest>
{
	private readonly IProductRepository _productRepository;

	public CreateProductRequestValidator(IProductRepository productRepository)
	{
		_productRepository = productRepository;

		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Product name is required.")
			.MinimumLength(3).WithMessage("Product name must be at least 3 characters.")
			.MaximumLength(50).WithMessage("Product name must not exceed 50 characters.");
			//.Must(MustBeAUniqueProductName).WithMessage("Product name must be unique."); // senkron yöntem
			//.MustAsync(MustBeAUniqueProductNameAsync).WithMessage("Product name must be unique."); // asenkron yöntem
		RuleFor(x => x.Price)
			.GreaterThan(0).WithMessage("Price must be greater than zero.");
		RuleFor(x => x.CategoryId)
			.GreaterThan(0).WithMessage("Category Id must be greater than zero.");

		RuleFor(x => x.Stock)
			.InclusiveBetween(1,200).WithMessage("Stock must be between 1 and 250.");
	}

	//// 1. Yol : senkron yöntem
	//private bool MustBeAUniqueProductName(string name)
	//{
	//	return !_productRepository.Where(x=>x.Name==name).Any();
	//}

	//// 3. Yol : asenkron yöntem
	//private async Task<bool> MustBeAUniqueProductNameAsync(string name, CancellationToken cancellationToken)
	//{
	//	return !await _productRepository.Where(x => x.Name.ToLower() == name.ToLower())
	//		.AnyAsync(cancellationToken);
	//}
}
