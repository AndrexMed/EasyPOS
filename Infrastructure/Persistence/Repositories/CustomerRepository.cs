using Domain.Customer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task Add(Customer customer) => await _appDbContext.AddAsync(customer);

        public async Task<Customer?> GetByIdAsync(CustomerId id) => await _appDbContext.Customers.SingleOrDefaultAsync(x => x.Id == id);
    }
}