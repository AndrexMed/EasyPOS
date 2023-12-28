namespace Domain.Customer
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(CustomerId id);
        Task Add(Customer customer);
    }
}
