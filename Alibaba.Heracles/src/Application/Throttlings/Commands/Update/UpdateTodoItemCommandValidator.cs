using System.Data;
using Alibaba.Heracles.Domain.Exceptions;
using Alibaba.Heracles.Domain.ValueObjects;
using FluentValidation;

namespace Alibaba.Heracles.Application.Throttlings.Commands.Update
{
    public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateThrottlingCommand>
    {
        public UpdateTodoItemCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Id is a positive integer");
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