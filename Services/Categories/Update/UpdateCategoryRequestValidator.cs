using FluentValidation;

namespace CleanEx.Services.Categories.Update
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Category name is required").NotEmpty().WithMessage("Category name cannot be empty").Length(1, 10).WithMessage("Category name must be between 1-10 characters");

            RuleFor(x => x.Description).NotNull().WithMessage("Category description is required").NotEmpty().WithMessage("Category description cannot be empty").Length(1, 100).WithMessage("Category description must be between 1-100 characters");
        }
    }
}
