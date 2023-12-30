using Domain.Customer;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Customers.Create
{
    public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Unit>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unityOfWork;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unityOfWork)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _unityOfWork = unityOfWork ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            if (PhoneNumber.Create(command.phoneNumber) is not PhoneNumber phoneNumber)
            {
                return Error.Validation("Customer.PhoneNumber ", "PhoneNumber has not valid format");
                //throw new ArgumentException(nameof(phoneNumber));
            }

            if (Address.Create(command.country,
                               command.line1,
                                command.line2,
                                 command.city,
                                  command.state,
                                   command.zipCode) is not Address address)
            {
                return Error.Validation("Customer.Address ", "Address is not valid");
                //throw new ArgumentException(nameof(address));
            }

            var customer = new Customer(
                new CustomerId(Guid.NewGuid()),
                command.name,
                command.lastName,
                command.email,
                phoneNumber,
                address,
                true
                );

            _customerRepository.Add(customer);
            await _unityOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}