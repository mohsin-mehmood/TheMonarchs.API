using FluentValidation;
using TheMonarchs.API.MediatR.House.Commands;

namespace TheMonarchs.API.MediatR.House.Validators
{
    public class GetMonarchsByHouseQueryValidator : AbstractValidator<FindMonarchsByHouseCommand>
    {
        public GetMonarchsByHouseQueryValidator()
        {

            RuleFor(h => h.HouseName)
                .NotEmpty()
                .WithMessage("House name is required.");
        }
    }
}
