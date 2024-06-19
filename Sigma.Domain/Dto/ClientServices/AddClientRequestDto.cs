using Beaver.Domain.Constants;
using FluentValidation;

namespace Sigma.Domain.Dto.ClientServices;

public class AddClientRequestDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? StartInterval { get; set; }
    public DateTime? EndInterval { get; set; }
    public string? LinkednURL { get; set; }
    public string? GithubURL { get; set; }
    public string Comment { get; set; }
}

public class AddClientRequestDtoValidation : AbstractValidator<AddClientRequestDto>
{
    public AddClientRequestDtoValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email can't be empty")
            .Matches(RegularExpressions.MailFormat)
            .WithMessage("Email format is invalid");
        
        When(x => !string.IsNullOrEmpty(x.GithubURL), () =>
        {
            RuleFor(x => x.GithubURL)
                .Matches(RegularExpressions.UrlFormat)
                .WithMessage("GithubURL format is invalid");
        });
        
        RuleFor(x => x.PhoneNumber)
            .Matches(RegularExpressions.PhoneNumberFormat)
            .WithMessage("PhoneNumber format is invalid");
        
        RuleFor(x => x.LinkednURL)
            .Matches(RegularExpressions.UrlFormat)
            .WithMessage("LinkednURL format is invalid");
        
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .Must(m => char.IsUpper(m[0]))
            .WithMessage("First name must start with an uppercase letter")
            .MaximumLength(20)
            .WithMessage("First name must be less than 20 characters long");
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .Must(m => char.IsUpper(m[0]))
            .WithMessage("Last name must start with an uppercase letter")
            .MaximumLength(20)
            .WithMessage("Last name must be less than 20 characters long");
        
        RuleFor(x => x.Comment)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Comment must be less than 100 characters long");
        
        When(x => x.StartInterval.HasValue && x.EndInterval.HasValue, () =>
        {
            RuleFor(x => x).Must((client, endInterval) => validTimes(client)).WithMessage("EndInterval must be greater than StartInterval.");
        });

        When(x => x.StartInterval.HasValue && !x.EndInterval.HasValue, () =>
        {
            RuleFor(x => x.StartInterval.Value)
                .LessThanOrEqualTo(DateTime.Now.AddDays(1)).WithMessage("StartInterval should be within the next 24 hours.");
        });

        When(x => x.EndInterval.HasValue && !x.StartInterval.HasValue, () =>
        {
            RuleFor(x => x.EndInterval.Value)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("EndInterval should not be in the past.");
        });
    }
    
    private bool validTimes(AddClientRequestDto client)
    { 
        return client.EndInterval.Value > client.StartInterval.Value;
    }
}