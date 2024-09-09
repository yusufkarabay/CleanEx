using FluentValidation;

namespace CleanEx.Services.Products
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Ürün ismi gereklidir").NotEmpty().WithMessage("Ürün ismi boş olamaz").Length(1, 10).WithMessage("Ürün ismi 1-10 karakter arasında olmalıdır");

            RuleFor(x => x.Description).NotNull().WithMessage("Ürün açıklaması gereklidir").NotEmpty().WithMessage("Ürün açıklaması boş olamaz").Length(1, 100).WithMessage("Ürün açıklaması 1-100 karakter arasında olmalıdır");
            RuleFor(x => x.Price).NotNull().WithMessage("Ürün fiyatı gereklidir").NotEmpty().WithMessage("Ürün fiyatı boş olamaz").GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır");
            RuleFor(x => x.Stock).NotNull().WithMessage("Ürün stok adedi gereklidir").NotEmpty().WithMessage("Ürün stok adedi boş olamaz").GreaterThan(0).WithMessage("Ürün stok adedi 0'dan büyük olmalıdır");


        }
    }
}
