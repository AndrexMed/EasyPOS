using ErrorOr;
using MediatR;

namespace Application.Customers.Create
{
    public record CreateCustomerCommand(string name,
                                        string lastName,
                                        string email,
                                        string phoneNumber,
                                        string country,
                                        string line1,
                                        string line2,
                                        string city,
                                        string state,
                                        string zipCode) : IRequest<ErrorOr<Unit>>
    {
    }
}
