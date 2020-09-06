using Alibaba.Heracles.Domain.Exceptions;
using Alibaba.Heracles.Domain.ValueObjects;
using FluentValidation;

namespace Alibaba.Heracles.Application.Throttlings.Commands.Create
{
    public class CreateThrottlingCommandValidator : AbstractValidator<CreateThrottlingCommand>
    {
        public CreateThrottlingCommandValidator()
        {
            RuleFor(x => x.LimitString)
                .NotNull().NotEmpty().WithMessage("Limit can not be empty");
            RuleFor(x => x.Pattern)
                .NotNull().NotEmpty().WithMessage("Pattern can not be empty");
            RuleFor(x => x.LimitString)
                .Custom((x, ctx) =>
                {
                    try
                    {
                        Limit.FromString(x);
                    }
                    catch (InvalidLimitStringException e)
                    {
                        ctx.AddFailure("Limit", e.Message);
                    }
                });

            // this part maybe needed to ignore for tests !!
            RuleFor(x => x.Pattern)
                .Matches("(http(s)?):\\/\\/.+")
                .WithMessage("Pattern should start with [http(s)://]");
        }
    }
}