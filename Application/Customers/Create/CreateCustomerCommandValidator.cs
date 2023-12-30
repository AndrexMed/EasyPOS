using FluentValidation;

namespace Application.Customers.Create
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(r => r.name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(r => r.lastName)
                 .NotEmpty()
                 .MaximumLength(50)
                 .WithName("Last Name");

            RuleFor(r => r.email)
                 .NotEmpty()
                 .EmailAddress()
                 .MaximumLength(255);

            RuleFor(r => r.phoneNumber)
                 .NotEmpty()
                 .MaximumLength(9)
                 .WithName("Phone Number");

            RuleFor(r => r.country)
                .NotEmpty()
                .MaximumLength(3);

            RuleFor(r => r.line1)
                .NotEmpty()
                .MaximumLength(20)
                .WithName("Addres Line 1");

            RuleFor(r => r.line2)
                .MaximumLength(20)
                .WithName("Addres Line 2");

            RuleFor(r => r.city)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(r => r.state)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(r => r.zipCode)
                .NotEmpty()
                .MaximumLength(10)
                .WithName("Zip Code");
        }

    }
}
