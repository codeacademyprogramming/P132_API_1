using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace StoreApi.Dtos.CategoryDtos
{
    public class CategoryPostDto
    {
        public string Name { get; set; }
    }

    public class CategoryPostDtoValidator : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(25);
        }
    }


}
