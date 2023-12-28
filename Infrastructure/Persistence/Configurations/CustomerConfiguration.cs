using Domain.Customer;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                    .HasConversion(
                       customerId => customerId.Value,
                       value => new CustomerId(value));

            builder.Property(x => x.Name)
                    .HasMaxLength(50);

            builder.Property(x => x.LastName)
                    .HasMaxLength(50);

            builder.Ignore(x => x.FullName);

            builder.Property(x => x.Email)
                    .HasMaxLength(255);

            builder.HasIndex(x => x.Email)
                    .IsUnique();

            builder.Property(c => c.PhoneNumber)
                    .HasConversion(
                         phoneNumber => phoneNumber.Value,
                          value => PhoneNumber.Create(value)!)
                    .HasMaxLength(9);

            builder.OwnsOne(c => c.Address, addressBuilder => {

                addressBuilder.Property(a => a.Country).HasMaxLength(3);
                addressBuilder.Property(a => a.Line1).HasMaxLength(20);
                addressBuilder.Property(a => a.Line2).HasMaxLength(20).IsRequired(false);
                addressBuilder.Property(a => a.City).HasMaxLength(40);
                addressBuilder.Property(a => a.State).HasMaxLength(40);
                addressBuilder.Property(a => a.ZipCode).HasMaxLength(10).IsRequired(false);
            });

            builder.Property(c => c.Active);
        }
    }
}
