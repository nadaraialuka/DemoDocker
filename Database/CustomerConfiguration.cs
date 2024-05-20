using DemoProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.Database
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", "Demo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsUnicode(true).HasMaxLength(30);
            builder.Property(x => x.LastName).IsUnicode(true).HasMaxLength(30);
        }
    }
}
