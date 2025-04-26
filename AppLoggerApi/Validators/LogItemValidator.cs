using FluentValidation;

namespace AppLoggerApi.Model;

public class LogItemValidator : AbstractValidator<LogItem>
{
    public LogItemValidator()
    {
        RuleFor(item => item.BusinessId)
        .Matches(@"^.{4,8}$")
        .WithMessage("Value was not a valid App Business ID");

        RuleFor(book => book.Message).NotEmpty();
        RuleFor(book => book.CreatedDate).NotEmpty();
    }

}
