using Domain.Customer;
using Domain.Primitives;
using Domain.ValueObjects;
using MediatR;

namespace Application.Customers.Create
{
    internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnityOfWork _unityOfWork;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnityOfWork unityOfWork)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _unityOfWork = unityOfWork ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<Unit> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            if (PhoneNumber.Create(command.phoneNumber) is not PhoneNumber phoneNumber)
            {
                throw new ArgumentException(nameof(phoneNumber));
            }

            if(Address.Create(command.country,
                               command.line1,
                                command.line2,
                                 command.city,
                                  command.state,
                                   command.zipCode) is not Address address)
            {
                throw new ArgumentException(nameof(address));
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

            await _customerRepository.Add(customer);
            await _unityOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
